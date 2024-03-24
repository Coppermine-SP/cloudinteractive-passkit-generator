let issueForm;
document.addEventListener('DOMContentLoaded', function () {
    issueForm = document.getElementById("issueForm");
});

function templateSelection() {
    issueForm.method = "GET";
    issueForm.submit();
}