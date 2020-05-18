var toast = new Toasty();

window.exploreGroups = {
    showSpinner: function () {
        $('#groupsSpinner').addClass('d-inline-block');
    },
    hideSpinner: function () {
        $('#groupsSpinner').removeClass('d-inline-block');
    }
};

window.exploreEvents = {
    showSpinner: function () {
        $('#eventsSpinner').addClass('d-inline-block');
    },
    hideSpinner: function () {
        $('#eventsSpinner').removeClass('d-inline-block');
    }
};

window.group = {
    hideGroupSpinner: function () {
        $('#groupSpinner').addClass('d-none');
    },
    showEventsSpinner: function () {
        $('#eventsSpinner').addClass('d-inline-block');
    },
    hideEventsSpinner: function () {
        $('#eventsSpinner').removeClass('d-inline-block');
    },
    showMembersSpinner: function () {
        $('#membersSpinner').addClass('d-inline-block');
    },
    hideMembersSpinner: function () {
        $('#membersSpinner').removeClass('d-inline-block');
    },
    preJoin: function () {
        $('#btnJoin').attr('disabled', 'true');
        $('#btnJoin').find('span').addClass('d-inline-block');
    },
    postSuccessJoin: function (message) {
        $('#btnJoin').find('span').removeClass('d-inline-block');

        toast.success(message);
    },
    postFailJoin: function () {
        $('#btnJoin').removeAttr('disabled');
        $('#btnJoin').find('span').removeClass('d-inline-block');

        toast.error('Hata oluştu');
    },
    preLeave: function () {
        $('#btnLeave').attr('disabled', 'true');
        $('#btnLeave').find('span').addClass('d-inline-block');
    },
    postSuccessLeave: function (message) {
        $('#btnLeave').find('span').removeClass('d-inline-block');

        toast.success(message);
    },
    postFailLeave: function () {
        $('#btnLeave').removeAttr('disabled');
        $('#btnLeave').find('span').removeClass('d-inline-block');

        toast.error('Hata oluştu');
    }
};

window.event = {
    hideEventSpinner: function () {
        $('#eventSpinner').addClass('d-none');
    },
    showCommentsSpinner: function () {
        $('#commentsSpinner').addClass('d-inline-block');
    },
    hideCommentsSpinner: function () {
        $('#commentsSpinner').removeClass('d-inline-block');
    },
    showAttendeesSpinner: function () {
        $('#attendeesSpinner').addClass('d-inline-block');
    },
    hideAttendeesSpinner: function () {
        $('#attendeesSpinner').removeClass('d-inline-block');
    },
    preJoin: function () {
        $('#btnJoin').attr('disabled', 'true');
        $('#btnJoin').find('span').addClass('d-inline-block');
    },
    postSuccessJoin: function (message) {
        $('#btnJoin').find('span').removeClass('d-inline-block');

        toast.success(message);
    },
    postFailJoin: function () {
        $('#btnJoin').removeAttr('disabled');
        $('#btnJoin').find('span').removeClass('d-inline-block');

        toast.error('Hata oluştu');
    },
    preLeave: function () {
        $('#btnLeave').attr('disabled', 'true');
        $('#btnLeave').find('span').addClass('d-inline-block');
    },
    postSuccessLeave: function (message) {
        $('#btnLeave').find('span').removeClass('d-inline-block');

        toast.success(message);
    },
    postFailLeave: function () {
        $('#btnLeave').removeAttr('disabled');
        $('#btnLeave').find('span').removeClass('d-inline-block');

        toast.error('Hata oluştu');
    },
    preAddComment: function () {
        $('#btnComment').attr('disabled', 'true');
        $('#btnComment').find('span').addClass('d-inline-block');
    },
    postSuccessAddComment: function (message) {
        $('#btnComment').find('span').removeClass('d-inline-block');

        toast.success(message);
    },
    postFailAddComment: function () {
        $('#btnComment').removeAttr('disabled');
        $('#btnComment').find('span').removeClass('d-inline-block');

        toast.error('Hata oluştu');
    }
};