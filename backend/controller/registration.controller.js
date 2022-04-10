const { RegistrationModel, EventModel } = require('../models');
const generateQr = require('../utils/generateQr');
const catchAsync = require('../utils/catchAsync');
const sendEmail = require('../services/email');

exports.registerForEvent = catchAsync(async (req, res) => {
  const user = req.user;
  const { ticketCount, eventId } = req.body;
  const currentCount = await RegistrationModel.count();

  const event = await EventModel.findOne({ 'subEvents._id': eventId });

  if (!event) {
    throw Error('Event not found');
  }

  const subEvent = event.subEvents.find(
    (subEvent) => `${subEvent._id}` === `${eventId}`
  );

  if (!subEvent) {
    throw Error('Event not found');
  }

  if (
    subEvent.totalSeats === subEvent.bookedSeates ||
    subEvent.bookedSeates + ticketCount > subEvent.totalSeats
  ) {
    throw Error('Not engough seats avaiable');
  }

  const qrCode = `00000${currentCount + 10}`.slice(-6);

  const registration = await RegistrationModel.create({
    userId: user._id,
    ticketCount,
    eventId,
    qrCodeId: qrCode,
  });

  await EventModel.findOneAndUpdate(
    { 'subEvent._id': eventId },
    { 'subEvent.$.totalSeats': { $inc: -ticketCount } }
  );

  const qrUrl = await generateQr({ code: qrCode });

  res.send({
    status: 'success',
    body: {
      qrCode: qrUrl,
      registration,
    },
  });
  const mailOptions = {
    subject: 'Your ticket is here!',
    to: user.email,
    attachments: [{ path: qrUrl }],
    html: '<h1>Here is your registered ticket',
  };
  sendEmail(mailOptions);
});

exports.verifyRegistrations = catchAsync(async (req, res) => {
  const { qrCodeId } = req.body;
  console.log(qrCodeId);
  const registration = await RegistrationModel.findOne({
    qrCodeId,
  }).populate('userId');

  if (!registration) {
    throw Error('Not registered');
  }
  const event = await EventModel.findOne({
    'subEvents._id': `${registration.eventId}`,
  });
  if (!event) {
    throw Error('Event not found');
  }

  const subEvent = event.subEvents.find(
    (subEvent) => `${subEvent._id}` === `${registration.eventId}`
  );
  res.send({
    status: 'success',
    body: {
      banner: event.banner,
      ticketCount: registration.ticketCount,
      code: registration.qrCodeId,
      username: registration.userId.name,
      eventName: event.name,
      subEventName: subEvent.name,
    },
  });
});

exports.getRegistedEvents = catchAsync(async (req, res) => {
  const { _id } = req.user;

  const registratedEvent = await RegistrationModel.find({ userId: _id });

  const qrPromises = registratedEvent.map((registration) => {
    return generateQr(registration.qrCodeId);
  });

  const qrCodes = await Promise.all(qrPromises);

  res.send({
    status: 'success',
    body: {
      token: req.headers['authorization'].split('Bearer ')[1],
      events: (registratedEvent || []).map((event, index) => ({
        eventId: event.eventId,
        qrCode: qrCodes[index],
      })),
    },
  });
});
