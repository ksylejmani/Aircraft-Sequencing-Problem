// Generated by CoffeeScript 1.7.1
define(['modules/clean/contacts/types'], function(ContactTypes) {
  var ContactsUtil;
  ContactsUtil = (function() {
    function ContactsUtil() {}

    ContactsUtil.activityUserToContact = function(activity_user, priority) {
      if (priority == null) {
        priority = 0;
      }
      return {
        dbx_account_id: activity_user.unique_id,
        fname: activity_user.fname,
        lname: activity_user.lname,
        name: activity_user.display_name,
        name_tokens: [activity_user.fname, activity_user.lname],
        email: activity_user.email,
        type: ContactTypes.DBX_ID,
        priority: priority,
        photo_url: activity_user.photo_circle_url
      };
    };

    return ContactsUtil;

  })();
  return ContactsUtil;
});

//# sourceMappingURL=util.map
