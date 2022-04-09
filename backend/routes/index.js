const router = require('express').Router();

router.use('/user', require('./user.routes'));
router.use('/event', require('./event.routes'));
router.use('/registration', require('./registration.routes'));

module.exports = router;
