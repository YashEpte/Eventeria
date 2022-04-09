const { UserModel } = require('../models');
const jwt = require('jsonwebtoken');
var bcrypt = require('bcryptjs');
const catchAsync = require('../utils/catchAsync');

exports.register = catchAsync(async (req, res, next) => {
  const { email, password, name } = req.body;

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

  res.send({
    status: 'success',
    body: { token },
  });
});
