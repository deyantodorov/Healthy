var Manager = (function () {

    // This function help you to transliterate what you need

    function postToTransliterator(getFrom, setTo, postUrl) {
        $(getFrom).change(function () {

            var title = $(getFrom).val();

            if (title != '') {
                $.ajax({
                    data: { "content": title },
                    type: 'POST',
                    cache: false,
                    url: postUrl,
                    success: function (data) {
                        $(setTo).val(data);
                    },
                    fail: function (data) {
                        $(setTo).val("Error");
                    }
                });
            }
        });
    }

    function searchForTag(getFrom, setTo, postUrl, onSuccess) {
        $(getFrom).change(function () {

            var search = $(getFrom).val();
            var testForTags = search.lastIndexOf(',');

            if (testForTags !== -1) {
                search = search.substr(testForTags + 1);
            }

            if (search !== '' && search.length >= 3) {
                $.ajax({
                    data: { "content": search },
                    type: 'POST',
                    cache: false,
                    url: postUrl,
                    success: function (data) {
                        onSuccess(data);
                    },
                    fail: function (data) {
                        $(setTo).html(data);
                    }
                });
            }
        });
    }

    function searchForImage(getFrom, setTo, postUrl, onSuccess) {
        $(getFrom).keypress(function () {

            var search = $(getFrom).val();

            if (search !== '' && search.length >= 3) {
                $.ajax({
                    data: { "content": search },
                    type: 'POST',
                    cache: false,
                    url: postUrl,
                    success: function (data) {
                        onSuccess(data);
                    },
                    fail: function (data) {
                        $(setTo).html(data);
                    }
                });
            }
        });
    }

    function displayError(msg) {

        var err = $('<div class="mt-30 pt-30 text-center"><div class="alert alert-danger">' + msg + '</div></div').fadeIn().fadeOut(3000);
        $('#manager').prepend(err);
    }

    return {
        PostTransliterator: postToTransliterator,
        SearchForTag: searchForTag,
        SearchForImage: searchForImage,
        DisplayError: displayError
    }
})();