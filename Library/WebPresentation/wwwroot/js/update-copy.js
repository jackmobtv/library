$(document).on("click", ".edit-modal-button", function () {
    let copyId = $(this).data('copyid');
    let condition = $(this).data('condition')
    console.log(condition);
    console.log(copyId);
    $(".edit-body #copyId").val(copyId);
    $(".edit-body #condition").val(condition);
});