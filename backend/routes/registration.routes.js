const router = require('express').Router();

const authMiddleware = require('../middleware/auth.middleware');
const { registrationController } = require('../controller');

router.post(
  '/register',
  authMiddleware,
  registrationController.registerForEvent
);

router.post('/verifyRegistrations', registrationController.verifyRegistrations);

module.exports = router;
