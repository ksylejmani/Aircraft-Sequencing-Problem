// Generated by CoffeeScript 1.7.1
define(['modules/dirty/streams/lib/user_queue'], function(UserQueue) {
  var EventbusDarkLaunch;
  EventbusDarkLaunch = (function() {
    EventbusDarkLaunch.prototype._num_queues = null;

    EventbusDarkLaunch.prototype._user_id = null;

    EventbusDarkLaunch._started_darklaunch_allocation = false;

    function EventbusDarkLaunch(_user_id, _num_queues) {
      this._user_id = _user_id;
      this._num_queues = _num_queues;
    }

    EventbusDarkLaunch.prototype.allocate_event_queues = function() {
      var queue_num, _i, _ref, _results;
      if (this._num_queues <= 0 || this.constructor._started_darklaunch_allocation) {
        return;
      }
      this.constructor._started_darklaunch_allocation = true;
      _results = [];
      for (queue_num = _i = 1, _ref = this._num_queues; 1 <= _ref ? _i <= _ref : _i >= _ref; queue_num = 1 <= _ref ? ++_i : --_i) {
        _results.push(UserQueue.get_user_queue(this._user_id, "/eventbus/darklaunch", {
          queue_num: queue_num
        }));
      }
      return _results;
    };

    return EventbusDarkLaunch;

  })();
  return EventbusDarkLaunch;
});

//# sourceMappingURL=eventbus_dark_launch.map