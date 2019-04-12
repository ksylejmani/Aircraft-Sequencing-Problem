// Generated by CoffeeScript 1.7.1
var __indexOf = [].indexOf || function(item) { for (var i = 0, l = this.length; i < l; i++) { if (i in this && this[i] === item) return i; } return -1; };

define(['external/react', 'modules/core/exception', 'modules/core/i18n', 'modules/core/notify', "modules/core/uri", 'modules/clean/activity/activity', 'modules/clean/activity/activity_local_storage', 'modules/clean/analytics', 'modules/clean/filepath', 'modules/clean/react/sprite', 'modules/clean/react/tooltip', 'modules/clean/react/file_comments/shared_link_signup_modals'], function(React, _arg, _arg1, Notify, URI, Activity, ActivityLocalStorage, _arg2, FilePath, Sprite, tooltip, CommentsSharedLinkSignupModals) {
  var ActivityContext, FileFeedbackLikesSection, MAX_OTHER_LIKERS_IN_TOOLTIP, WebLightboxLogger, assert, d, ungettext, _;
  assert = _arg.assert;
  _ = _arg1._, ungettext = _arg1.ungettext;
  WebLightboxLogger = _arg2.WebLightboxLogger;
  d = React.DOM;
  ActivityContext = Activity.ActivityContext;
  MAX_OTHER_LIKERS_IN_TOOLTIP = 15;
  FileFeedbackLikesSection = React.createClass({
    propTypes: {
      contextActivityStore: React.PropTypes.object.isRequired,
      activity: React.PropTypes.object,
      height: React.PropTypes.number,
      user: React.PropTypes.object
    },
    componentDidMount: function() {
      this._logEvent(WebLightboxLogger.LIKES_EXPERIMENT_VIEW);
      if (this.props.activity != null) {
        this._logEvent(WebLightboxLogger.LIKES_EXPERIMENT_LOADED);
      }
      if (!this.props.user.is_signed_in) {
        return this.modals = new CommentsSharedLinkSignupModals();
      }
    },
    componentDidUpdate: function(prevProps, prevState) {
      var activity_changed, store;
      activity_changed = (this.props.activity && this.props.activity.activity_key) !== (prevProps.activity && prevProps.activity.activity_key);
      if (activity_changed) {
        if (this.props.activity != null) {
          this._logEvent(WebLightboxLogger.LIKES_EXPERIMENT_LOADED);
        }
        if (!this.props.activity.feedback_off) {
          if (this.modals) {
            this.modals.set_activity_key(this.props.activity.activity_key);
          }
        }
        if (this.props.user.is_signed_in) {
          store = ActivityLocalStorage.get_store("like", this.props.activity.activity_key);
          if (store && !store.comment_activity_key) {
            ActivityLocalStorage.clear_store("like");
            this.props.contextActivityStore.add_like(this.props.activity, this.props.user, this._onActivityUpdate);
            return this._logEvent(WebLightboxLogger.LIKES_EXPERIMENT_COMPLETED_LIKE);
          }
        }
      }
    },
    render: function() {
      var like, like_button_hover_sprite, like_button_on_click, like_button_sprite, other_likers, _i, _len, _ref;
      if (this.props.activity == null) {
        return this._renderEmptyView();
      }
      like_button_sprite = this._isLikedByViewingUser() ? "like_icon_selected" : "like_icon";
      like_button_hover_sprite = this._isLikedByViewingUser() ? "like_icon_selected_hover" : "like_icon_hover";
      like_button_on_click = this._isLikedByViewingUser() ? this._removeLike : this._addLike;
      other_likers = [];
      _ref = this.props.activity.likes;
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        like = _ref[_i];
        if (!this.props.user.is_signed_in || like.liker.id !== this.props.user.id) {
          other_likers.push(like.liker);
        }
      }
      return d.div({
        className: "file-feedback-likes-section file-feedback-section",
        style: {
          height: this.props.height
        }
      }, d.a({
        className: "like-button",
        onClick: like_button_on_click,
        onMouseEnter: this._onLikeButtonMouseEnter
      }, d.div({
        className: "like-button-icon-container"
      }, Sprite({
        group: "web",
        name: like_button_sprite,
        className: "like-button-icon"
      }), Sprite({
        group: "web",
        name: like_button_hover_sprite,
        className: "like-button-icon-hover"
      })), d.div({
        className: "like-button-text"
      }, _("Like"))), other_likers.length > 0 ? this._renderOtherLikersView(other_likers) : void 0);
    },
    _renderOtherLikersView: function(other_likers) {
      var html_escaped_first_likers_name, liker, other_likers_desc, other_likers_in_tooltip, other_likers_tooltip_contents;
      assert(other_likers.length > 0);
      html_escaped_first_likers_name = other_likers[0].fname.escapeHTML();
      if (other_likers.length === 1) {
        other_likers_desc = _("<span class=\"blue\">%(name)s</span> likes this").format({
          name: html_escaped_first_likers_name
        });
      } else {
        other_likers_desc = ungettext("<span class=\"blue\">%(name)s and %(num)s other</span> like this", "<span class=\"blue\">%(name)s and %(num)s others</span> like this", other_likers.length - 1).format({
          name: html_escaped_first_likers_name,
          num: other_likers.length - 1
        });
      }
      other_likers_in_tooltip = other_likers.slice(0, MAX_OTHER_LIKERS_IN_TOOLTIP);
      if (other_likers.length > MAX_OTHER_LIKERS_IN_TOOLTIP) {
        other_likers_in_tooltip.push({
          id: "extra",
          display_name: _("%(num_extra)d more...").format({
            num_extra: other_likers.length - MAX_OTHER_LIKERS_IN_TOOLTIP
          })
        });
      }
      other_likers_tooltip_contents = d.div({
        className: "other-liker-names"
      }, (function() {
        var _i, _len, _results;
        _results = [];
        for (_i = 0, _len = other_likers_in_tooltip.length; _i < _len; _i++) {
          liker = other_likers_in_tooltip[_i];
          _results.push((function(_this) {
            return function(liker) {
              return d.div({
                key: liker.id
              }, liker.display_name);
            };
          })(this)(liker));
        }
        return _results;
      }).call(this));
      return [
        d.div({
          className: "separator-dot"
        }, "\u00b7"), tooltip.Tooltip({
          position: tooltip.TooltipPosition.BOTTOM,
          tooltip_contents: other_likers_tooltip_contents,
          tooltip_classname: "other-likers-tooltip"
        }, d.div({
          className: "other-likers",
          dangerouslySetInnerHTML: {
            __html: other_likers_desc
          },
          onMouseEnter: this._onOtherLikersMouseEnter
        }))
      ];
    },
    _renderEmptyView: function() {
      return d.div({
        className: "file-feedback-likes-section file-feedback-section",
        style: {
          height: this.props.height
        }
      }, d.a({
        className: "like-button loading"
      }, d.div({
        className: "like-button-icon-container"
      }, Sprite({
        group: "web",
        name: "like_icon_empty"
      }), d.img({
        src: "/static/images/icons/ajax-loading-small-blue-vfl6t1QvH.gif",
        className: "like-button-icon-loading"
      })), d.div({
        className: "like-button-text"
      }, _("Like"))));
    },
    _isLikedByViewingUser: function() {
      var like, _ref;
      if (this.props.user.is_signed_in) {
        return _ref = this.props.user.id, __indexOf.call((function() {
          var _i, _len, _ref1, _results;
          _ref1 = this.props.activity.likes;
          _results = [];
          for (_i = 0, _len = _ref1.length; _i < _len; _i++) {
            like = _ref1[_i];
            _results.push(like.liker.id);
          }
          return _results;
        }).call(this), _ref) >= 0;
      } else {
        return false;
      }
    },
    _addLike: function() {
      var error_callback;
      this._logEvent(WebLightboxLogger.LIKES_EXPERIMENT_CLICKED_LIKE);
      if (this.props.user.is_signed_in) {
        error_callback = (function(_this) {
          return function() {
            return Notify.error(_("We weren\u2019t able to submit your like."));
          };
        })(this);
        this.props.contextActivityStore.add_like(this.props.activity, this.props.user, this._onActivityUpdate, null, error_callback);
        return this._logEvent(WebLightboxLogger.LIKES_EXPERIMENT_COMPLETED_LIKE);
      } else {
        ActivityLocalStorage.set_store("like", this.props.activity.activity_key);
        return this.modals.show_sign_up_modal();
      }
    },
    _removeLike: function() {
      var error_callback;
      this._logEvent(WebLightboxLogger.LIKES_EXPERIMENT_CLICKED_UNLIKE);
      error_callback = (function(_this) {
        return function() {
          return Notify.error(_("We weren\u2019t able to remove your like."));
        };
      })(this);
      return this.props.contextActivityStore.remove_like(this.props.activity, this.props.user, this._onActivityUpdate, null, error_callback);
    },
    _onActivityUpdate: function(updated_activity) {
      return this.setState({
        activity: updated_activity
      });
    },
    _onLikeButtonMouseEnter: function() {
      return this._logEvent(WebLightboxLogger.LIKES_EXPERIMENT_MOUSED_OVER_LIKE);
    },
    _onOtherLikersMouseEnter: function() {
      return this._logEvent(WebLightboxLogger.LIKES_EXPERIMENT_MOUSED_OVER_OTHERS);
    },
    _logEvent: function(event_name) {
      return WebLightboxLogger.log(event_name, {
        context: this._getContextForLogging(),
        file_ext: this._getFileExtForLogging(),
        num_likes: this.props.activity != null ? this.props.activity.likes.length : -1,
        liked_by_viewer: this.props.activity != null ? this._isLikedByViewingUser() : false,
        file_key: this.props.activity != null ? this.props.activity.activity_key : "none"
      });
    },
    _getContextForLogging: function() {
      switch (this.props.contextActivityStore.activity_context) {
        case ActivityContext.BROWSE_LIGHTBOX:
          return "browse_lightbox";
        case ActivityContext.SHARED_LINK_VIEW:
          return "shmodel_view";
        case ActivityContext.SHARED_LINK_LIGHTBOX:
          return "shmodel_lightbox";
        default:
          return assert(false);
      }
    },
    _getFileExtForLogging: function() {
      var parsed_uri;
      switch (this.props.contextActivityStore.activity_context) {
        case ActivityContext.BROWSE_LIGHTBOX:
          return FilePath.file_extension_for_logging(this.props.contextActivityStore.activity_context_data);
        case ActivityContext.SHARED_LINK_VIEW:
        case ActivityContext.SHARED_LINK_LIGHTBOX:
          parsed_uri = URI.parse(this.props.contextActivityStore.activity_context_data);
          return FilePath.file_extension_for_logging(parsed_uri.getPath());
        default:
          return assert(false);
      }
    }
  });
  return FileFeedbackLikesSection;
});

//# sourceMappingURL=file_feedback_likes_section.map