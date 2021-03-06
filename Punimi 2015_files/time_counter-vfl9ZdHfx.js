// Generated by CoffeeScript 1.7.1
define(['external/react', 'modules/clean/datetime'], function(React, DateTime) {
  var TimeCounter, d;
  d = React.DOM;
  TimeCounter = React.createClass({
    render: function() {
      var time_ago_str;
      time_ago_str = DateTime.ago(new Date(this.props.when_mses));
      return d.div({
        className: "activity-time-ago"
      }, time_ago_str);
    },
    updateLoop: function() {},
    componentDidMount: function() {
      return this.updateLoop();
    },
    getUpdateInterval: function() {
      return 5000;
    }
  });
  return TimeCounter;
});

//# sourceMappingURL=time_counter.map
