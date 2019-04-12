// Generated by CoffeeScript 1.7.1
var __indexOf = [].indexOf || function(item) { for (var i = 0, l = this.length; i < l; i++) { if (i in this && this[i] === item) return i; } return -1; },
  __slice = [].slice;

define(['jquery', 'external/underscore', 'modules/core/exception', 'modules/core/uri', 'modules/clean/viewer'], function($j, $u, _arg, URI, Viewer) {
  var NOTCLIENTS, NOTCLIENT_DEBUG, NotServer, SUBSCRIBE_URL, assert, user, _i, _len, _ref;
  assert = _arg.assert;
  NOTCLIENTS = {};
  NOTCLIENT_DEBUG = false;
  SUBSCRIBE_URL = '/subscribe';
  NotServer = (function() {
    function NotServer() {}

    NotServer.prototype.add_notclient = function(user_id, nid, ns_map) {
      this.new_notclient(user_id);
      return setTimeout((function() {
        return NOTCLIENTS[user_id].init(nid, ns_map);
      }), 2000);
    };

    NotServer.prototype.new_notclient = function(user_id) {
      var HANDLER_WAIT_MS, INITIAL_CHILLAGE_MS, MAX_CHILLAGE_MS, MAX_URL_LENGTH, SFJ_HANDLER_TYPE, USER_HANDLER_TYPE, abort, aborted, build_subscribe_params, connect, connect_timeout_id, consecutive_bad_rounds, done_handling, handler_map, initiated, is_connected, is_handling, is_nonnegative_int, is_positive_int, log, new_nid, new_ns_map, next_handler_id, nid, notclient, notserver_chillout_ms, ns_map, one_or_more_handler_failures, reconnect, report_slackers, reset, run_handlers, slacker_timeout_id, sleeping_for, total_rounds, unfinished_handler_ids, update_ns_map, xhr, _connect;
      assert(__indexOf.call(NOTCLIENTS, user_id) < 0, 'cannot create more than one notclient per user');
      is_positive_int = function(n) {
        return $u.isNumber(n) && n % 1 === 0 && n > 0;
      };
      is_nonnegative_int = function(n) {
        return $u.isNumber(n) && n % 1 === 0 && n >= 0;
      };
      assert(is_positive_int(user_id), 'user_id must be a positive integer');
      log = function() {
        var msgs;
        msgs = 1 <= arguments.length ? __slice.call(arguments, 0) : [];
        if (NOTCLIENT_DEBUG) {
          return console.log.apply(console, msgs);
        }
      };
      HANDLER_WAIT_MS = 90000;
      MAX_URL_LENGTH = 8096;
      USER_HANDLER_TYPE = 'user';
      SFJ_HANDLER_TYPE = 'list';
      initiated = false;
      INITIAL_CHILLAGE_MS = 1000;
      MAX_CHILLAGE_MS = 5 * 60 * 1000;
      notserver_chillout_ms = 0;
      sleeping_for = function() {
        var notclient_chillout_ms;
        if (consecutive_bad_rounds === 0) {
          notclient_chillout_ms = 0;
        } else {
          notclient_chillout_ms = Math.min(INITIAL_CHILLAGE_MS * Math.pow(2, consecutive_bad_rounds - 1), MAX_CHILLAGE_MS);
        }
        return Math.max(notclient_chillout_ms, notserver_chillout_ms);
      };
      nid = null;
      ns_map = {};
      handler_map = {};
      next_handler_id = 1;
      is_connected = false;
      is_handling = false;
      xhr = null;
      aborted = false;
      unfinished_handler_ids = [];
      slacker_timeout_id = null;
      connect_timeout_id = null;
      new_nid = null;
      new_ns_map = {};
      one_or_more_handler_failures = false;
      consecutive_bad_rounds = 0;
      total_rounds = 0;
      update_ns_map = function(ns_map, new_ns_map) {
        var ns_id, sjid, _results;
        _results = [];
        for (ns_id in new_ns_map) {
          sjid = new_ns_map[ns_id];
          ns_id = parseInt(ns_id, 10);
          assert(is_positive_int(ns_id), "ns_ids must be positive integers: " + ns_id);
          assert(is_nonnegative_int(sjid), "sjids must be nonnegative integers: " + sjid);
          if (ns_map[ns_id] != null) {
            _results.push(ns_map[ns_id] = Math.min(ns_map[ns_id], sjid));
          } else {
            _results.push(ns_map[ns_id] = sjid);
          }
        }
        return _results;
      };
      build_subscribe_params = function() {
        var handler_types, ns, params, sjid;
        assert((nid != null) && !$u.isEmpty(ns_map), 'expected nid and ns_map');
        params = {
          host_int: 0,
          trace: window.location.pathname,
          rev: Constants.SVN_REV
        };
        handler_types = $u.pluck($u.values(handler_map), 'type');
        if (__indexOf.call(handler_types, USER_HANDLER_TYPE) >= 0) {
          params.user_id = user_id;
          params.nid = nid != null ? nid.replace(/^0+(.)/, '$1') : void 0;
        }
        if (__indexOf.call(handler_types, SFJ_HANDLER_TYPE) >= 0) {
          params.ns_map = ((function() {
            var _results;
            _results = [];
            for (ns in ns_map) {
              sjid = ns_map[ns];
              _results.push("" + ns + "_" + sjid);
            }
            return _results;
          })()).join(',');
        }
        if (URI.parse(SUBSCRIBE_URL).updateQuery(params).toString().length > MAX_URL_LENGTH) {
          delete params.ns_map;
        }
        return params;
      };
      connect = function() {
        var sleep;
        if (!initiated) {
          return;
        }
        sleep = sleeping_for();
        if (sleep > 0) {
          return connect_timeout_id = window.setTimeout(_connect, sleep);
        } else {
          return _connect();
        }
      };
      _connect = function() {
        var params;
        assert(!is_connected && !is_handling, 'connect: invalid state');
        assert(nid >= 0 || !$u.isEmpty(ns_map), "notclient: called connect with nothing to subscribe to");
        log('###########################');
        params = build_subscribe_params();
        if ((params.nid == null) && !params.ns_map) {
          log("nothing to subscribe to. skipping notserver connection.");
          return;
        }
        log('connecting to notserver...');
        is_connected = true;
        total_rounds += 1;
        return xhr = $j.ajax(SUBSCRIBE_URL, {
          data: params,
          dataType: 'json',
          noDropboxDefaults: true,
          error: function() {
            if (aborted) {
              aborted = false;
              return;
            }
            consecutive_bad_rounds += 1;
            log("error connecting to notserver. bad rounds=" + consecutive_bad_rounds + ".");
            is_connected = false;
            return connect();
          },
          success: function(response) {
            log("notserver connection closed. response:", response);
            is_connected = false;
            if (response.chillout != null) {
              notserver_chillout_ms = parseInt(response.chillout, 10) * 1000;
              log("notserver told us to chill for " + notserver_chillout_ms + "ms");
            } else if (notserver_chillout_ms > 0) {
              log("setting notserver chillout back to 0ms");
              notserver_chillout_ms = 0;
            }
            if (response.ret === 'punt') {
              return connect();
            } else {
              assert(response.ret === 'new', "unknown notserver ret: " + response.ret);
              assert('refresh' in response, 'expected notserver ret:new to have refresh keyword');
              return run_handlers(response.refresh);
            }
          }
        });
      };
      abort = function() {
        if (xhr != null) {
          aborted = true;
          xhr.abort();
          return xhr = null;
        }
      };
      reconnect = function() {
        notserver_chillout_ms = 0;
        is_connected = false;
        is_handling = false;
        abort();
        unfinished_handler_ids = [];
        window.clearTimeout(slacker_timeout_id);
        window.clearTimeout(connect_timeout_id);
        slacker_timeout_id = null;
        connect_timeout_id = null;
        new_nid = null;
        new_ns_map = {};
        one_or_more_handler_failures = false;
        consecutive_bad_rounds = 0;
        return connect();
      };
      reset = function() {
        var old_handler_cutoff;
        initiated = false;
        notserver_chillout_ms = 0;
        nid = null;
        ns_map = {};
        handler_map = {};
        old_handler_cutoff = 0;
        next_handler_id = 1;
        is_connected = false;
        is_handling = false;
        xhr = null;
        aborted = false;
        unfinished_handler_ids = [];
        window.clearTimeout(slacker_timeout_id);
        window.clearTimeout(connect_timeout_id);
        slacker_timeout_id = null;
        connect_timeout_id = null;
        new_nid = null;
        new_ns_map = {};
        one_or_more_handler_failures = false;
        consecutive_bad_rounds = 0;
        return total_rounds = 0;
      };
      run_handlers = function(types) {
        var handler, handler_id, name, triggered_handlers, type, _i, _len, _ref;
        assert(!is_handling && !is_connected, 'run_handlers: invalid state');
        assert(new_nid === null, 'run_handlers: new_nid must start at null');
        assert($u.isEqual(new_ns_map, {}), 'run_handlers: new_ns_map must start at {}');
        assert(!one_or_more_handler_failures, 'expected one_or_more_handler_failures=false');
        is_handling = true;
        log('running handlers...');
        triggered_handlers = $u.filter($u.values(handler_map), function(handler_info) {
          var _ref;
          return _ref = handler_info.type, __indexOf.call(types, _ref) >= 0;
        });
        assert(!$u.isEmpty(triggered_handlers), "notserver sent a ping for unsubscribed activity");
        unfinished_handler_ids = $u.pluck(triggered_handlers, 'handler_id');
        for (_i = 0, _len = triggered_handlers.length; _i < _len; _i++) {
          _ref = triggered_handlers[_i], handler = _ref.handler, handler_id = _ref.handler_id, name = _ref.name, type = _ref.type;
          log("running id=" + handler_id + ", name=" + name + ", type=" + type);
          handler();
        }
        if ($u.isEmpty(unfinished_handler_ids)) {
          return log("all handlers already finished running. no need for a slacker timeout.");
        } else {
          log("all handlers running. slacker timeout set.");
          return slacker_timeout_id = window.setTimeout(report_slackers, HANDLER_WAIT_MS);
        }
      };
      report_slackers = function() {
        var handler_id, _i, _len, _results;
        assert(is_handling && !is_connected, 'called report_slackers in an invalid state.');
        assert(!$u.isEmpty(unfinished_handler_ids, "report_slackers called w/ nothing slackin'"));
        log("found some slackers");
        one_or_more_handler_failures = true;
        _results = [];
        for (_i = 0, _len = unfinished_handler_ids.length; _i < _len; _i++) {
          handler_id = unfinished_handler_ids[_i];
          _results.push(done_handling(handler_id));
        }
        return _results;
      };
      done_handling = function(handler_id) {
        if (__indexOf.call(unfinished_handler_ids, handler_id) < 0) {
          return;
        }
        log("done handling: " + handler_id);
        unfinished_handler_ids = $u.without(unfinished_handler_ids, handler_id);
        if ($u.isEmpty(unfinished_handler_ids)) {
          log("all handlers are done running.");
          is_handling = false;
          window.clearTimeout(slacker_timeout_id);
          if (new_nid != null) {
            log("new nid: " + new_nid);
            nid = new_nid;
          }
          if (!$u.isEmpty(new_ns_map)) {
            log("new ns_map:", new_ns_map);
            ns_map = new_ns_map;
          }
          if (one_or_more_handler_failures) {
            consecutive_bad_rounds += 1;
            log("one or more handler errors. bad_rounds=" + consecutive_bad_rounds);
          } else {
            consecutive_bad_rounds = 0;
          }
          new_nid = null;
          new_ns_map = {};
          one_or_more_handler_failures = false;
          return connect();
        }
      };
      notclient = {
        get_user_id: function() {
          return user_id;
        },
        get_nid: function() {
          return nid;
        },
        get_ns_map: function() {
          return $u.clone(ns_map);
        },
        get_consecutive_bad_rounds: function() {
          return consecutive_bad_rounds;
        },
        get_total_rounds: function() {
          return total_rounds;
        },
        get_notserver_chillout_ms: function() {
          return notserver_chillout_ms;
        },
        get_sleep_ms: sleeping_for,
        subscribe_user: function(params) {
          var handler_id, handler_types, need_reconnect, param, _i, _len, _ref;
          assert(!is_handling, 'adding new handlers from inside handlers is not currently supported');
          _ref = ['name', 'handler'];
          for (_i = 0, _len = _ref.length; _i < _len; _i++) {
            param = _ref[_i];
            assert(params[param], "subscribe_user requires this param be non-falsey: " + param);
          }
          assert($u.isFunction(params.handler), 'handler must be a function');
          log("adding user handler " + params.name);
          handler_types = $u.pluck($u.values(handler_map), 'type');
          need_reconnect = __indexOf.call(handler_types, USER_HANDLER_TYPE) < 0;
          handler_id = next_handler_id++;
          handler_map[handler_id] = {
            handler_id: handler_id,
            handler: params.handler,
            name: params.name,
            type: USER_HANDLER_TYPE
          };
          if (need_reconnect) {
            log("first user handler added, reconnecting");
            reconnect();
          }
          return handler_id;
        },
        subscribe_sfj: function(params) {
          var handler_id, handler_types, need_reconnect, param, _i, _len, _ref;
          assert(!is_handling, 'adding new handlers from inside handlers is not currently supported');
          _ref = ['name', 'handler'];
          for (_i = 0, _len = _ref.length; _i < _len; _i++) {
            param = _ref[_i];
            assert(params[param], "subscribe_sfj requires this param be non-falsey: " + param);
          }
          assert($u.isFunction(params.handler), 'handler must be a function');
          log("adding sfj handler " + params.name);
          handler_types = $u.pluck($u.values(handler_map), 'type');
          need_reconnect = __indexOf.call(handler_types, SFJ_HANDLER_TYPE) < 0;
          handler_id = next_handler_id++;
          handler_map[handler_id] = {
            handler_id: handler_id,
            handler: params.handler,
            name: params.name,
            type: SFJ_HANDLER_TYPE
          };
          if (need_reconnect) {
            log("first sfj handler added, reconnecting");
            reconnect();
          }
          return handler_id;
        },
        handler_success: function(handler_id, params) {
          var handler_type;
          if (__indexOf.call(unfinished_handler_ids, handler_id) < 0) {
            return;
          }
          handler_type = handler_map[handler_id].type;
          if (handler_type === SFJ_HANDLER_TYPE) {
            assert('ns_map' in params, 'ns_map required param is missing');
            assert(!('nid' in params), 'nid is disallowed from SFJ handlers');
            update_ns_map(new_ns_map, params.ns_map);
          } else {
            assert(handler_type === USER_HANDLER_TYPE, "unknown handler type: " + handler_type);
            assert('nid' in params, 'nid required param is missing');
            assert(!('ns_map' in params), 'ns_map is disallowed from user handlers');
            if ((new_nid == null) || params.nid < new_nid) {
              new_nid = params.nid;
            }
          }
          return done_handling(handler_id);
        },
        handler_failure: function(handler_id) {
          log("handler failed. handler_id=" + handler_id);
          if (__indexOf.call(unfinished_handler_ids, handler_id) < 0) {
            return;
          }
          one_or_more_handler_failures = true;
          return done_handling(handler_id);
        },
        handle_visibility_change: function() {
          if (window.document.hidden) {
            return abort();
          } else {
            return reconnect();
          }
        },
        init: function(initial_nid, initial_ns_map) {
          assert(!initiated, 'error: init() has been called twice.');
          nid = initial_nid;
          update_ns_map(ns_map, initial_ns_map);
          initiated = true;
          if (window.document.visibilityState != null) {
            document.addEventListener('visibilitychange', this.handle_visibility_change);
            if (!window.document.hidden) {
              return connect();
            }
          } else {
            return connect();
          }
        },
        reset: reset,
        abort: abort,
        reconnect: reconnect
      };
      NOTCLIENTS[user_id] = notclient;
      return notclient;
    };

    return NotServer;

  })();
  NotServer = new NotServer();
  _ref = Viewer.get_viewer().get_users();
  for (_i = 0, _len = _ref.length; _i < _len; _i++) {
    user = _ref[_i];
    NotServer.add_notclient(user.id, user.nid, user.ns_map);
  }
  return {
    NotServer: NotServer,
    NotClients: NOTCLIENTS
  };
});

//# sourceMappingURL=notserver.map
