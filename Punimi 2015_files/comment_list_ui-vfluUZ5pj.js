// Generated by CoffeeScript 1.7.1
var __indexOf = [].indexOf || function(item) { for (var i = 0, l = this.length; i < l; i++) { if (i in this && this[i] === item) return i; } return -1; };

define(['jquery', 'external/react', 'external/underscore', 'modules/core/i18n', 'modules/clean/activity/activity', 'modules/clean/activity/activity_local_storage', 'modules/clean/activity/activity_user', 'modules/clean/datetime', 'modules/clean/gandalf_util', 'modules/clean/react/button', 'modules/clean/react/file_comments/comment_list_header', 'modules/clean/react/file_comments/logger', 'modules/clean/react/image', 'modules/clean/react/react_i18n', 'modules/clean/react/sprite', 'modules/clean/react/activity/comment_activity_ui', 'modules/clean/react/activity/comment_input', 'modules/clean/react/file_comments/shared_link_signup_modals'], function($j, React, $u, _arg, Activity, ActivityLocalStorage, ActivityUser, DateTime, GandalfUtil, ButtonClass, CommentListHeaderClass, FileActivityClientLogger, ImageClass, ReactI18n, SpriteClass, CommentActivityUIClass, CommentInputClass, CommentsSharedLinkSignupModals) {
  var Button, CommentActivityUI, CommentInput, CommentListHeader, CommentListUI, Image, R_, ReactCSSTransitionGroup, Sprite, d, _;
  _ = _arg._;
  d = React.DOM;
  R_ = ReactI18n.R_;
  ReactCSSTransitionGroup = React.addons.CSSTransitionGroup;
  Button = React.createFactory(ButtonClass.button);
  CommentActivityUI = React.createFactory(CommentActivityUIClass);
  CommentInput = React.createFactory(CommentInputClass);
  CommentListHeader = React.createFactory(CommentListHeaderClass);
  Image = React.createFactory(ImageClass);
  ReactCSSTransitionGroup = React.createFactory(ReactCSSTransitionGroup);
  Sprite = React.createFactory(SpriteClass);
  CommentListUI = React.createClass({
    displayName: "CommentListUI",
    propTypes: {
      contextActivityStore: React.PropTypes.object.isRequired,
      activity: React.PropTypes.object,
      contactSearchLogger: React.PropTypes.object,
      onInputFocus: React.PropTypes.func,
      onInputBlur: React.PropTypes.func,
      height: React.PropTypes.number,
      user: React.PropTypes.object,
      onHideCommentsPane: React.PropTypes.func.isRequired,
      turnOffFeedbackCallback: React.PropTypes.func.isRequired,
      turnOnFeedbackCallback: React.PropTypes.func.isRequired,
      enableFacepile: React.PropTypes.bool,
      enableNoNotifyHint: React.PropTypes.bool,
      forceNullStateLoadingState: React.PropTypes.bool,
      shouldInitiallyFocusInput: React.PropTypes.bool,
      showBottomBar: React.PropTypes.bool,
      isNewCommentLoading: React.PropTypes.bool,
      showNewComment: React.PropTypes.bool,
      isSender: React.PropTypes.bool,
      showResolvedComments: React.PropTypes.bool,
      isUserSubscribed: React.PropTypes.bool,
      isCommentInputDisabled: React.PropTypes.bool,
      fileViewerState: React.PropTypes.object,
      checkScroll: React.PropTypes.func,
      onAddCommentFromList: React.PropTypes.func,
      onAddSticker: React.PropTypes.func,
      onShowNewComment: React.PropTypes.func,
      onDeleteCommentFromList: React.PropTypes.func,
      onResolveCommentFromList: React.PropTypes.func,
      onLikeComment: React.PropTypes.func,
      onToggleShowResolvedComments: React.PropTypes.func,
      toggleFeedbackForViewer: React.PropTypes.func,
      onFacepileContactAdded: React.PropTypes.func,
      onAnnotationButtonMouseUp: React.PropTypes.func,
      verifyAfterSignUpCallback: React.PropTypes.func,
      shouldShowHideButton: React.PropTypes.bool,
      shouldAutoLinkify: React.PropTypes.bool,
      enableImport: React.PropTypes.bool,
      shouldUseSimpleModals: React.PropTypes.bool,
      shouldHidePhotoAvatars: React.PropTypes.bool
    },
    getInitialState: function() {
      if (!this.props.user.is_signed_in) {
        this.modals = new CommentsSharedLinkSignupModals();
      }
      return {
        contactsShouldDropdown: true
      };
    },
    getDefaultProps: function() {
      return {
        forceNullStateLoadingState: false,
        isCommentInputDisabled: false,
        fileViewerState: null,
        checkScroll: $j.noop,
        onShowNewComment: $j.noop,
        showBottomBar: false,
        shouldAutoLinkify: true,
        enableImport: true,
        shouldUseSimpleModals: false,
        shouldHidePhotoAvatars: false
      };
    },
    componentWillUnmount: function() {
      return $j(window).unbind("resize.bottom_bar");
    },
    componentDidMount: function() {
      var _base;
      $j(window).bind("resize.bottom_bar", (function(_this) {
        return function() {
          var _base;
          return typeof (_base = _this.props).checkScroll === "function" ? _base.checkScroll() : void 0;
        };
      })(this));
      this.checkSize();
      return typeof (_base = this.props).checkScroll === "function" ? _base.checkScroll() : void 0;
    },
    componentDidUpdate: function(prevProps, prevState) {
      var ca, is_new_context, like, store, stored_subscribe, _base, _i, _len, _ref, _ref1, _ref2, _ref3;
      if (!this._isActivityFetchDone()) {
        return;
      }
      is_new_context = (this.props.activity && this.props.activity.activity_key) !== (prevProps.activity && prevProps.activity.activity_key);
      if (is_new_context) {
        if (this.props.user.is_signed_in) {
          store = ActivityLocalStorage.get_store("like", this.props.activity.activity_key);
          if (store && store.comment_activity_key) {
            _ref = this.props.activity.comment_activities;
            for (_i = 0, _len = _ref.length; _i < _len; _i++) {
              ca = _ref[_i];
              if (ca.activity_key === store.comment_activity_key) {
                ActivityLocalStorage.clear_store("like");
                if (_ref1 = this.props.user.id, __indexOf.call((function() {
                  var _j, _len1, _ref2, _results;
                  _ref2 = ca.likes;
                  _results = [];
                  for (_j = 0, _len1 = _ref2.length; _j < _len1; _j++) {
                    like = _ref2[_j];
                    _results.push(like.liker.id);
                  }
                  return _results;
                })(), _ref1) < 0) {
                  this.props.contextActivityStore.add_comment_like(ca, this.props.user, null, null);
                }
              }
            }
          }
        }
        if (this.props.user.is_signed_in && !this.props.isUserSubscribed) {
          stored_subscribe = ActivityLocalStorage.get_store("subscribe", this.props.activity.activity_key);
          ActivityLocalStorage.clear_store("subscribe");
          if (stored_subscribe) {
            this.toggleFeedbackForViewer();
          }
        }
      }
      if (((prevProps.activity == null) && ((_ref2 = this.props.activity) != null ? (_ref3 = _ref2.comment_activities) != null ? _ref3.length : void 0 : void 0) > 0) || ((prevState.commentBoxHeight == null) && this.getCommentBoxHeight())) {
        this.animateScrollToBottom(500);
      }
      this.checkSize();
      return typeof (_base = this.props).checkScroll === "function" ? _base.checkScroll() : void 0;
    },
    onAddCommentFromList: function(text, metadata) {
      var _base;
      if (metadata == null) {
        metadata = {};
      }
      return typeof (_base = this.props).onAddCommentFromList === "function" ? _base.onAddCommentFromList(text, metadata) : void 0;
    },
    onLikeComment: function(commentActivity) {
      var _base;
      return typeof (_base = this.props).onLikeComment === "function" ? _base.onLikeComment(commentActivity) : void 0;
    },
    onResolveCommentFromList: function(commentActivity, resolved) {
      var _base;
      return typeof (_base = this.props).onResolveCommentFromList === "function" ? _base.onResolveCommentFromList(commentActivity, resolved) : void 0;
    },
    onDeleteCommentFromList: function(commentActivity) {
      var _base;
      return typeof (_base = this.props).onDeleteCommentFromList === "function" ? _base.onDeleteCommentFromList(commentActivity) : void 0;
    },
    onToggleShowResolvedComments: function() {
      var _base;
      return typeof (_base = this.props).onToggleShowResolvedComments === "function" ? _base.onToggleShowResolvedComments() : void 0;
    },
    onFacepileContactAdded: function($suggestion) {
      var _base;
      return typeof (_base = this.props).onFacepileContactAdded === "function" ? _base.onFacepileContactAdded($suggestion) : void 0;
    },
    toggleFeedbackForViewer: function() {
      var _base;
      return typeof (_base = this.props).toggleFeedbackForViewer === "function" ? _base.toggleFeedbackForViewer() : void 0;
    },
    isScrolledToBottom: function() {
      var $target;
      $target = $j(this.refs.commentList.getDOMNode());
      return $target[0].scrollHeight - $target.scrollTop() - 4 <= $target.outerHeight();
    },
    checkSize: function() {
      var commentBoxHeight;
      commentBoxHeight = this.getCommentBoxHeight();
      if (this.state.commentBoxHeight !== commentBoxHeight) {
        return this.setState({
          commentBoxHeight: commentBoxHeight
        });
      }
    },
    _verifyAfterSignUpCallback: function() {
      var _base;
      return typeof (_base = this.props).verifyAfterSignUpCallback === "function" ? _base.verifyAfterSignUpCallback() : void 0;
    },
    onAnnotationButtonMouseUp: function(commentActivity, commentMetdata) {
      var _base;
      return typeof (_base = this.props).onAnnotationButtonMouseUp === "function" ? _base.onAnnotationButtonMouseUp(commentActivity, commentMetdata) : void 0;
    },
    onAddSticker: function(stickerId) {
      var _base;
      return typeof (_base = this.props).onAddSticker === "function" ? _base.onAddSticker(stickerId) : void 0;
    },
    onScroll: function() {
      var _base;
      return typeof (_base = this.props).checkScroll === "function" ? _base.checkScroll() : void 0;
    },
    turnOnFeedback: function() {
      return this.props.turnOnFeedbackCallback();
    },
    turnOffFeedback: function() {
      return this.props.turnOffFeedbackCallback();
    },
    _isActivityFetchDone: function() {
      if (this.props.activity) {
        return true;
      } else {
        return false;
      }
    },
    onFocus: function() {
      var _base;
      if (GandalfUtil.getGandalfRule("dw-comments-expanded-input")) {
        setTimeout((function(_this) {
          return function() {
            return _this.forceUpdate();
          };
        })(this), 10);
      }
      return typeof (_base = this.props).onInputFocus === "function" ? _base.onInputFocus() : void 0;
    },
    resizeCallback: function() {
      return this.forceUpdate();
    },
    animateScrollToBottom: function(duration) {
      var comment_list_node;
      if (duration == null) {
        duration = 600;
      }
      comment_list_node = this.refs.commentList.getDOMNode();
      return $j(comment_list_node).animate({
        scrollTop: comment_list_node.scrollHeight
      }, {
        duration: duration
      });
    },
    scrollToBottom: function() {
      var comment_list_node;
      comment_list_node = this.refs.commentList.getDOMNode();
      return comment_list_node.scrollTop = comment_list_node.scrollHeight;
    },
    getCommentBoxHeight: function() {
      if (this.refs.commentInput != null) {
        return $j(this.refs.commentInput.getDOMNode()).outerHeight();
      } else {
        return 0;
      }
    },
    positionDropdownCallback: function() {
      var list_height;
      list_height = $j(this.refs.commentList.getDOMNode()).height();
      return this.setState({
        popupsShouldDropdown: Boolean(list_height < this.props.height / 2)
      });
    },
    render: function() {
      var ca, canEditFeedback, comment_list_classes, comments, comments_holder_classes, feedbackOff, holder_style, list_style, loadingSpinnerUI, newCommentLoadingClasses, num_resolved, _i, _len, _ref;
      loadingSpinnerUI = Image({
        src: "icons/ajax-loading-small-blue.gif"
      });
      comments = [];
      num_resolved = 0;
      if (this._isActivityFetchDone()) {
        _ref = this.props.activity.comment_activities;
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          ca = _ref[_i];
          if (ca.comment.resolved) {
            num_resolved += 1;
          }
          if (!ca.comment.resolved || this.props.showResolvedComments) {
            comments.push(CommentActivityUI({
              key: ca.activity_key,
              comment_activity: ca,
              context_activity_store: this.props.contextActivityStore,
              user: this.props.user,
              fileViewerState: this.props.fileViewerState,
              shouldUseSimpleModals: this.props.shouldUseSimpleModals,
              shouldHidePhotoAvatars: this.props.shouldHidePhotoAvatars,
              onLikeComment: this.onLikeComment,
              delete_comment: this.onDeleteCommentFromList,
              onAnnotationButtonMouseUp: this.onAnnotationButtonMouseUp,
              update_resolved: this.onResolveCommentFromList,
              shouldAutoLinkify: this.props.shouldAutoLinkify
            }));
          }
        }
      }
      comment_list_classes = React.addons.classSet({
        "comment-list": true,
        "scrolled": this.props.showBottomBar
      });
      comments_holder_classes = React.addons.classSet({
        "comments-holder": true,
        "comments-activity-loaded": this._isActivityFetchDone(),
        "comments-holder-responsive": GandalfUtil.getGandalfRule("dw-comments-responsive-width")
      });
      if (this.props.height) {
        holder_style = {
          "height": this.props.height
        };
        if (this.state.commentBoxHeight != null) {
          list_style = {
            "maxHeight": this.props.height - this.state.commentBoxHeight
          };
        }
      }
      feedbackOff = this._isActivityFetchDone() && this.props.activity.feedback_off;
      canEditFeedback = this._isActivityFetchDone() && this.props.activity.can_edit_feedback;
      return d.div({
        className: comments_holder_classes,
        ref: "commentsHolder",
        style: holder_style
      }, d.div({
        className: comment_list_classes,
        ref: "commentList",
        onScroll: this.onScroll,
        style: list_style
      }, CommentListHeader({
        show_resolved: this.props.showResolvedComments,
        toggle_show_resolved_callback: this.onToggleShowResolvedComments,
        hide_callback: this.props.onHideCommentsPane,
        is_subscribed: this.props.isUserSubscribed,
        onSubscribeButtonClicked: this.toggleFeedbackForViewer,
        num_resolved: num_resolved,
        shouldShowHideButton: this.props.shouldShowHideButton,
        shouldShowDisableButton: !feedbackOff && canEditFeedback,
        feedback_off: feedbackOff,
        turn_off_feedback_callback: this.turnOffFeedback,
        activity_context: this.props.contextActivityStore.activity_context,
        activity: this.props.activity,
        user: this.props.user,
        onFacepileContactAdded: this.onFacepileContactAdded,
        enableFacepile: this.props.enableFacepile
      }), !this._isActivityFetchDone() && !this.props.forceNullStateLoadingState ? d.div({
        className: "comments-loading"
      }, loadingSpinnerUI) : void 0, this._isActivityFetchDone() && this.props.activity.feedback_off ? d.div({
        className: "blank-state"
      }, d.div({
        className: "blank-state-icon"
      }, Sprite({
        group: "web",
        name: "s_comments_locked"
      })), d.div({
        className: "blank-state-text"
      }, R_({}, "Comments are off for this file.")), d.div({
        className: "blank-state-button"
      }, Button({
        importance: "secondary",
        onMouseUp: this.turnOnFeedback
      }, R_({}, "Turn on comments")))) : comments.length > 0 ? ReactCSSTransitionGroup({
        transitionName: "comment"
      }, comments) : (this._isActivityFetchDone() && !this.props.isNewCommentLoading) || (!this._isActivityFetchDone() && this.props.forceNullStateLoadingState) ? d.div({
        className: "blank-state"
      }, d.div({
        className: "blank-state-illustration"
      }, Image({
        src: "commenting/comments_null_state.png"
      })), d.div({
        className: "blank-state-text"
      }, R_({}, "Post a comment to start a discussion. <span class=\"blue\">@Mention</span> someone to notify them."))) : void 0, this.props.isNewCommentLoading ? (newCommentLoadingClasses = React.addons.classSet({
        "loading-comment": true,
        "blank-state": comments.length === 0
      }), d.div({
        className: newCommentLoadingClasses
      }, loadingSpinnerUI)) : void 0), ((this._isActivityFetchDone() && !this.props.activity.feedback_off) || (!this._isActivityFetchDone() && this.props.forceNullStateLoadingState)) && (!this.props.isCommentInputDisabled) ? CommentInput({
        activity: this.props.activity,
        commentCallback: this.onAddCommentFromList,
        contactSearchLogger: this.props.contactSearchLogger,
        onAddSticker: this.onAddSticker,
        verifyAfterSignUpCallback: this._verifyAfterSignUpCallback,
        ref: "commentInput",
        resizeCallback: this.resizeCallback,
        positionDropdownCallback: this.positionDropdownCallback,
        popupsShouldDropdown: this.state.popupsShouldDropdown,
        onFocus: this.onFocus,
        onBlur: this.props.onInputBlur,
        user: this.props.user,
        onShowNewComment: this.props.onShowNewComment,
        showNewComment: this.props.showNewComment,
        inBlankState: comments.length === 0,
        enableNotifyText: !this.props.enableFacepile,
        enableNoNotifyHint: this.props.enableNoNotifyHint,
        commentMetadataAllowed: GandalfUtil.getGandalfRule("pptx-commenting") || GandalfUtil.getGandalfRule("dw-comments-stickers"),
        enableStickers: GandalfUtil.getGandalfRule("dw-comments-stickers"),
        fileViewerState: this.props.fileViewerState,
        shouldInitiallyFocusInput: this.props.shouldInitiallyFocusInput,
        isCommentInputDisabled: this.props.isCommentInputDisabled,
        enableImport: this.props.enableImport,
        shouldHidePhotoAvatars: this.props.shouldHidePhotoAvatars
      }) : void 0);
    }
  });
  return CommentListUI;
});

//# sourceMappingURL=comment_list_ui.map
