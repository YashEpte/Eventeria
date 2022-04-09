const { EventModel } = require('../models');
const catchAsync = require('../utils/catchAsync');

exports.createEvent = catchAsync(async (req, res) => {
  const { name, description, subEvents } = req.body;

  const banner = req.file.filename;

  const event = await EventModel.create({
    name,
    description,
    subEvents: JSON.parse(subEvents),
    banner,
  });

  res.send({
    status: 'success',
    body: { event },
  });
});

exports.getAllUpcomingEvents = catchAsync(async (req, res) => {
  const events = await EventModel.find({
    'subEvents.date': { $gt: Date.now() },
  });

  res.send({
    status: 'success',
    body: { events },
  });
});
