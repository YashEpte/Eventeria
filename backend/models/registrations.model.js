const mongoose = require('mongoose');

const RegistrationSchema = mongoose.Schema({
  qrCodeId: {
    type: String,
    required: true,
    unique: true,
  },
  userId: {
    type: mongoose.Types.ObjectId,
    required: true,
    ref: 'users',
  },
  ticketCount: {
    type: Number,
    required: true,
  },
});

module.exports = mongoose.model('registrations', RegistrationSchema);
