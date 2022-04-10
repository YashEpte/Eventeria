const mongoose = require('mongoose');

const SubEventSchema = mongoose.Schema({
  name: {
    type: String,
    required: true,
  },
  description: {
    type: String,
    required: true,
  },
  venue: {
    type: String,
    required: true,
  },
  date: {
    type: Date,
    required: true,
  },
  price: {
    type: Number,
    required: true,
  },
  totalSeats: {
    type: Number,
    required: true,
  },
  bookedSeates: {
    type: Number,
    default: 0,
  },
});

const EventSchema = mongoose.Schema({
  name: {
    type: String,
    required: true,
  },
  description: {
    type: String,
    required: true,
  },
  isFeatured: {
    type: Boolean,
    default: false,
  },
  banner: {
    type: String,
    required: true,
  },
  categories: [String],
  subEvents: [SubEventSchema],
});

module.exports = mongoose.model('events', EventSchema);
