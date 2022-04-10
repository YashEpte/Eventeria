const { UserModel, EventModel, RegistrationModel } = require('../models');
const jwt = require('jsonwebtoken');
var bcrypt = require('bcryptjs');
const catchAsync = require('../utils/catchAsync');
const generateQr = require('../utils/generateQr');

exports.register = catchAsync(async (req, res, next) => {
  const { email, password, name } = req.body;
  // if (email === 'bhavin.divecha09@gmail.com') {
  //   return res.send({
  //     status: 'success',
  //     body: {
  //       token:
  //         'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImJoYXZpbi5kaXZlY2hhMDlAZ21haWwuY29tIiwiX2lkIjoiNjI1MTRiMmZlYmIxOGE5MWI4ZGJjYzA1IiwibmFtZSI6IkJoYXZsbyIsImlhdCI6MTY0OTQ5NTMxNH0.MqcUaAE0bNZJ5Igu9clkWo4VCd_tjm2cZ2-sdoZmtRI',
  //     },
  //   });
  // }

  const existingUser = await UserModel.findOne({ email });

  if (existingUser) {
    throw Error('Email is already registered');
  }
  var passwordHash = bcrypt.hashSync(password);

  let user = await UserModel.create({ email, password: passwordHash, name });

  const token = jwt.sign(
    { email: user.email, _id: user._id, name: user.name },
    process.env.JWT_SALT
  );

  res.send({
    status: 'success',
    body: { token },
  });
});

exports.login = catchAsync(async (req, res, next) => {
  const { email, password } = req.body;

  const user = await UserModel.findOne({ email });

  if (!user) {
    throw Error('Email is not registered');
  }

  const isValid = bcrypt.compareSync(password, user.password);

  if (!isValid) {
    throw Error('Password is incorrect');
  }

  const token = jwt.sign(
    { email: user.email, _id: user._id, name: user.name },
    process.env.JWT_SALT
  );

  const registratedEvent = await RegistrationModel.find({ userId: user._id });
  const qrPromises = registratedEvent.map((registration) => {
    return generateQr(registration.qrCodeId);
  });

  const qrCodes = await Promise.all(qrPromises);

  res.send({
    status: 'success',
    body: {
      token,
      events: (registratedEvent || []).map((event, index) => ({
        eventId: event._id,
        qrCode: qrCodes[index],
      })),
    },
  });
});
