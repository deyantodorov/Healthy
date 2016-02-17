// Transliterate Title
Site.PostTransliterator('#Title', '#Alias', '/Manager/Home/Transliterate');

// OnClick populate AddDate in PublishedDate
$('#IsPublished').bind('change', function () {
    if ($(this).is(':checked')) {
        var addDate = $('#AddDate').val();
        $('#PublishDate').val(addDate);
    } else {
        $('#PublishDate').val('');
    }
});

// Search for Image
Site.SearchForTag('#imageSearch', '#imageSearchResult', '/Manager/Article/SearchForImage', onImageSuccess);

// Search for Tags
Site.SearchForTag('#Tags', '#tagSearchResult', '/Manager/Article/SearchForTag', onTagSuccess);

function onTagSuccess(data) {
    var fragment = $(document.createDocumentFragment());

    for (var i = 0; i < data.length; i++) {
        var obj = data[i];
        for (var key in obj) {
            var attrValue = obj[key];
            var element = $('<span>');
            element.text(attrValue);
            element.on('click', addToTags);
            element.addClass('btn btn-default mr-15 mb-15');
            fragment.append(element);
        }
    }

    $('#tagSearchResult').append(fragment);
}

function onImageSuccess(data) {
    var fragment = $(document.createDocumentFragment());

    for (var i = 0; i < data.length; i++) {
        var element = $('<img>');
        element.attr('src', data[i]["Path"]);
        element.attr('width', '100');
        element.attr('alt', data[i]["Id"]);
        element.attr('title', data[i]["Desc"]);
        element.on('click', addToImages);
        element.addClass('mr-15 mb-15 img-thumbnail pointer');
        fragment.append(element);
    }

    $('#imageSearchResult').append(fragment);
}

function addToTags() {
    var current = $('#Tags').val() + $(this).text() + ', ';
    $(this).removeClass('btn-default').addClass('btn-info');
    $('#Tags').val(current);
}

function addToImages() {
    $('#ImageId').val($(this).attr('alt'));
    $(this).parent().children().removeClass('img-circle');
    $(this).toggleClass('img-thumbnail').toggleClass('img-circle');
}