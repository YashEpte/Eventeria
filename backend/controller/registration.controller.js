const QRCode = require('qrcode');
const { RegistrationModel } = require('../models');
const catchAsync = require('../utils/catchAsync');

exports.registerForEvent = catchAsync(async (req, res) => {
  const user = req.user;
  const { ticketCount, eventId } = req.body;
  const currentCount = await RegistrationModel.count();

  const qrCode = `00000${currentCount + 1}`.slice(-6);

  const registration = await RegistrationModel.create({
    userId: user._id,
    ticketCount,
    eventId,
    qrCodeId: qrCode,
  });

  QRCode.toDataURL(`${qrCode}`, function (err, url) {
    if (err) {
      console.log(err);
      throw Error(err);
    }

    res.send({
      status: 'success',
      body: {
        qr: url,
        registration,
      },
    });
  });
});

exports.verifyRegistrations = catchAsync(async (req, res) => {
  const { qrCodeId } = req.body;
  const registration = await RegistrationModel.findOne({
    qrCodeId,
  });

  if (!registration) {
    throw Error('Not registered');
  }

  res.send({
    status: 'success',
    body: {
      registration,
    },
  });
});
