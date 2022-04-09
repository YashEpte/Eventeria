const mongoose = require('mongoose');

const RegistrationSchema = mongoose.Schema({
  userId: {
    type: mongoose.Types.ObjectId,
    required: true,
    ref: 'users',
  },
  ticketCount: {
    type: Number,
    required: true,
  },
  eventId: {
    type: mongoose.Types.ObjectId,
    required: true,
    ref: 'events',
  },
  qrCodeId: {
    type: String,
    required: true,
    unique: true,
  },
});

module.exports = mongoose.model('registrations', RegistrationSchema);
