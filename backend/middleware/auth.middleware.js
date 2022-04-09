const jwt = require('jsonwebtoken');

const authMiddleware = (req, res, next) => {
  const authHeader = req.headers['authorization'];

  if (!authHeader || !authHeader.includes('Bearer')) {
    throw Error('You are not authenticated');
  }

  const token = authHeader.split('Bearer ')[1];
  if (!token) throw Error('You are not authenticated');

  jwt.verify(token, process.env.JWT_SALT, (err, user) => {
    if (err) {
      throw Error(err);
    }

    req.user = user;
    next();
  });
};

module.exports = authMiddleware;
