function loadUserRoleForm(source, id) {
    globalFunctions.loadPopup(source, "/Admin/EditChapter?id=" + id,  "Add User");

}
$('#myModal').on('shown.bs.modal', function () {
    $('#myInput').trigger('focus')
})