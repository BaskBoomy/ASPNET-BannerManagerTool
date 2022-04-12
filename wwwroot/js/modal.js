/*
 * [모달 숨기기 기능]
 * 없어도될듯..
 */
function hideModal() {
    $('.modal').hide();
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();
}
/*
 * [다음 모달 띄우는 기능]
 * - Placeholderhere에 원하는 모달 띄우기
 */
var PlaceHolderElement = $('#PlaceHolderHere');
$(function () {

    $('button[data-toggle="ajax-modal"], img[data-toggle="ajax-modal"], input[data-toggle="ajax-modal"], a[data-toggle= "ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        var decodeUrl = decodeURIComponent(url);
        $.get(decodeUrl).done(function (data) {
            $('.modal').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        })
    })

    var myModalEl = document.getElementById('selectTemplateModal')
    myModalEl.addEventListener('hidden.bs.modal', function (event) {
        PlaceHolderElement.html("");
        window.location.reload();
    })
})
/*
 * [템플릿 데이터 입력 기능]
 */
var ids = document.querySelectorAll('[id]');
Array.prototype.forEach.call(ids, function (el, i) {
    var arr = el.id.split('-');
    var dataId;
    if (arr[0] === 'data') {
        dataId = el.id;
        if (arr[1] === 'text') {
            $('#' + dataId).keyup(function () {
                console.log(arr[2]);
                $('#' + arr[2]).html($(this).val());
            })
        } else if (arr[1] === 'videourl') {
            $('#' + dataId).keyup(function () {
                var video = document.getElementById("videoTag");
                var mp4source = document.getElementById("mp4Src");
                var webmsource = document.getElementById("webmSrc");
                video.pause();
                video.currentTime = 0;
                mp4source.src = $(this).val();
                webmsource.src = $(this).val();
                video.load();
                video.play();
            })
        } else if (arr[1] === 'imageurl') {
            //arr[2] :  pc(pc해상도),m(모바일 해상도)
            //console.log(dataId);
            if (arr[2] === "pc") {
                $('#' + dataId).keyup(function () {
                    document.getElementById(arr[3]).style.backgroundImage = "url(" + $(this).val() + ")";
                })
            } else if (arr[2] === "m") {
                $('#' + dataId).keyup(function () {
                    document.getElementById(arr[3]).src = $(this).val();
                })
            } else {
                $('#' + dataId).keyup(function () {
                    document.getElementById(arr[2]).style.backgroundImage = "url(" + $(this).val() + ")";
                })
            }
            
        } else if (arr[1] === 'btnlink') {
            $('#' + dataId).keyup(function () {
                document.getElementById(arr[2]).href = $(this).val();
            })
        } else if (arr[1] === 'imagelink') {
            $('#' + dataId).keyup(function () {
                document.getElementById(arr[2]).href = $(this).val();
            })
        }
        //console.log(arr[2]);
    }
});

//$('#saveTemplateData').click(function () {
//    var htmlCode = document.getElementsByClassName('preview-frame')[0].innerHTML;
//    document.getElementById('HtmlCode').val = htmlCode.toString();
//    console.log(document.getElementById('HtmlCode').val);
//    document.getElementById('templateForm').submit();
//})
/*
 * [수정한 HTML 코드 가져오기]
 */
function setHtmlValue() {
    var htmlCode = document.getElementsByClassName('preview-frame')[0].innerHTML;
    document.getElementById('CodeHtml').value = htmlCode;
    return true;
}

/*
 * [템플릿 데이터 INSERT 기능]
 * - INSERT
 * - 다음 모달 띄우기
 */
function tpSubmit() {
    setHtmlValue();
    $.ajax({
        type: "POST",
        url: "/Home/TemplateData",
        data: jQuery("#templateForm").serialize(),
        success: function (res) {
            if (res) {
                PlaceHolderElement.html(res);
                $('.modal').modal('hide');
                $('body').removeClass('modal-open');
                $('.modal-backdrop').remove();
                PlaceHolderElement.find('.modal').modal('show');
            } else {
                location.reload();
            }
        },
        error: function (request, status, error) {
            alert("code:" + request.status + "\n" + "message:" + request.responseText + "\n" + "error:" + error);
        },
        complete: function (res) {
            alert("완료!");
        }
    })
}

/*
 * [Checkbox를 활용한 Edit, Delete 기능]
 */
$(document).ready(function () {
    $('input:checkbox[name="checkBoxAll"]').change(function () {
        var pageId = this.value;
        if ($(this).is(":checked")) {
            $('.chkCheckBoxId_' + pageId).prop('checked', true);
            document.getElementById('deleteBtn_' + pageId).style.display = "block";
        } else {
            $('.chkCheckBoxId_' + pageId).prop('checked', false);
            document.getElementById('deleteBtn_' + pageId).style.display = "none";
        }
    });

    $('input:checkbox[name="bannerId"]').change(function () {
        var val = this.value;
        var bannerId = val.split(',')[0];
        var pageId = val.split(',')[1];
        if ($("input:checkbox[class=chkCheckBoxId_"+pageId+"]:checked").length > 0) {
            document.getElementById('deleteBtn_' + pageId).style.display = "block";
            document.getElementById('editBtn_' + pageId).style.display = "block";
            if ($("input:checkbox[class=chkCheckBoxId_" + pageId + "]:checked").length >= 2) {
                document.getElementById('editBtn_' + pageId).style.display = "none";
            }
            else {
                $("input:checkbox[class=chkCheckBoxId_" + pageId + "]:checked").each(function () {
                    if (this.checked) {
                        document.getElementById('editBtn_' + pageId).setAttribute("data-url", "/Home/EditBanner/" + bannerId);
                    }
                })
            }
        } else {
            document.getElementById('deleteBtn_' + pageId).style.display = "none";
            document.getElementById('editBtn_' + pageId).style.display = "none";
        }



        
    });

    //$('input:checkbox[name="bannerId"]').change(function () {
    //    if ($('input:checkbox[name="bannerId"]:checked').length > 0) {
    //        deleteBtn_1.style.display = "block";
    //        editBtn_1.style.display = "block";

    //        if ($('input:checkbox[name="bannerId"]:checked').length >= 2) {
    //            editBtn_1.style.display = "none";
    //        }
    //        else {
    //            $('input:checkbox[name="bannerId"]:checked').each(function () {
    //                if (this.checked) {
    //                    editBtn_1.setAttribute("data-url", "/Home/EditBanner/" + this.value);
    //                }
    //            })
    //        }
    //    } else {
    //        deleteBtn_1.style.display = "none";
    //        editBtn_1.style.display = "none";
    //    }
    //})












    //$('input:checkbox[name="checkBoxAll"]').change(function () {
    //    var pageId = this.value;
        
    //    $('input:checkbox[name="checkBoxAll"]').each(function () {
            
            
    //        if ($(this).is(":checked")) {
    //            console.log(pageId);
    //            $('.chkCheckBoxId_' + pageId).prop('checked', true);
    //            document.getElementById('deleteBtn_'+pageId).style.display = "block";
    //            return false;
    //        } else {
    //            $('.chkCheckBoxId_' + pageId).prop('checked', false);
    //            document.getElementById('deleteBtn_' + pageId).style.display = "none";
    //            return false;
    //        }
    //    })
    //})

   
    //var pageId = 1;
        
        //$('#checkBoxAll_' + pageId).click(function () {
        //    console.log(pageId);
        //    if ($(this).is(":checked")) {
        //        deleteBtn.style.display = "block";
        //        $('.chkCheckBoxId_' + pageId).prop('checked', true);
        //    }
        //    else {
        //        deleteBtn.style.display = "none";
        //        $('.chkCheckBoxId_' + pageId).prop('checked', false);
        //    }
        //})
    


        
})