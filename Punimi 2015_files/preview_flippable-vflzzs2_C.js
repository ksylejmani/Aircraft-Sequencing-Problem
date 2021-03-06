// Generated by CoffeeScript 1.7.1
define(['external/react', 'modules/clean/react/previews/preview_blank', 'modules/clean/react/previews/preview_image', 'modules/clean/react/previews/preview_video', 'modules/clean/react/sprite', 'modules/core/i18n', 'modules/core/uri'], function(React, PreviewBlank, _arg, _arg1, Sprite, i18n, URI) {
  var PreviewFlippable, PreviewImage, PreviewVideo, cx, d, _;
  PreviewImage = _arg.PreviewImage;
  PreviewVideo = _arg1.PreviewVideo;
  d = React.DOM;
  cx = React.addons.classSet;
  _ = i18n._;
  PreviewFlippable = React.createClass({
    statics: {
      isFlippable: function(previewType) {
        return previewType === "photo" || previewType === "video";
      }
    },
    displayName: "PreviewFlippable",
    propTypes: {
      "preview-type": React.PropTypes.oneOf(["photo", "video"]),
      "file-extension": React.PropTypes.string.isRequired,
      "thumbnail-url-tmpl": React.PropTypes.string.isRequired,
      onLoad: React.PropTypes.func,
      onError: React.PropTypes.func,
      index: React.PropTypes.number,
      count: React.PropTypes.number,
      onPrevious: React.PropTypes.func,
      onNext: React.PropTypes.func,
      imagesToPreload: React.PropTypes.array,
      "preview-url": React.PropTypes.string,
      "video-metadata-url": React.PropTypes.string,
      "file-meta": React.PropTypes.shape({
        filename: React.PropTypes.string,
        mtime: React.PropTypes.number,
        formattedSize: React.PropTypes.string
      }).isRequired
    },
    _canFlipPrevious: function() {
      return this.props.index > 0;
    },
    _canFlipNext: function() {
      return this.props.index + 1 < this.props.count;
    },
    _onPrevious: function() {
      var _base;
      return typeof (_base = this.props).onPrevious === "function" ? _base.onPrevious() : void 0;
    },
    _onNext: function() {
      var _base;
      return typeof (_base = this.props).onNext === "function" ? _base.onNext() : void 0;
    },
    _reactElementForPreviewType: function(previewType) {
      var fileMeta;
      switch (previewType) {
        case "photo":
          return React.createElement(PreviewImage, {
            "thumbnail-url-tmpl": this.props["thumbnail-url-tmpl"],
            "file-extension": this.props["file-extension"],
            "file-meta": this.props["file-meta"],
            imagesToPreload: this.props.imagesToPreload,
            index: this.props.index,
            count: this.props.count,
            onPrevious: this._onPrevious,
            onNext: this._onNext,
            onLoad: this.props.onLoad,
            onError: this.props.onError
          });
        case "video":
          return React.createElement(PreviewVideo, {
            "preview-url": this.props["preview-url"],
            "thumbnail-url-tmpl": this.props["thumbnail-url-tmpl"],
            onLoad: this.props.onLoad
          });
        default:
          fileMeta = this.props["file-meta"];
          return React.createElement(PreviewBlank, {
            filename: fileMeta.filename,
            created: new Date(fileMeta.mtime * 1000),
            size: fileMeta.formattedSize
          });
      }
    },
    render: function() {
      var flipControlsClassSet;
      return d.div({
        className: "preview-flip-container"
      }, [
        this._reactElementForPreviewType(this.props["preview-type"]), this.props.count > 1 ? (flipControlsClassSet = {
          "flip-controls": true,
          "flip-controls--prev": this._canFlipPrevious(),
          "flip-controls--next": this._canFlipNext()
        }, d.div({
          className: cx(flipControlsClassSet)
        }, d.div({
          className: "flip-button-prev",
          onClick: this._onPrevious
        }, Sprite({
          group: "web",
          name: "s_flip_left"
        })), d.div({
          className: "flip-label"
        }, "" + (this.props.index + 1) + " of " + this.props.count), d.div({
          className: "flip-button-next",
          onClick: this._onNext
        }, Sprite({
          group: "web",
          name: "s_flip_right"
        })))) : void 0
      ]);
    }
  });
  return {
    PreviewFlippable: PreviewFlippable
  };
});

//# sourceMappingURL=preview_flippable.map
