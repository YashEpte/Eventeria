const router = require('express').Router();
const multer = require('../services/multer');
const { eventController } = require('../controller');
const authMiddleware = require('../middleware/auth.middleware');

router.post(
  '/',
  authMiddleware,
  multer.single('banner'),
  eventController.createEvent
);
router.get('/', eventController.getAllUpcomingEvents);

module.exports = router;
