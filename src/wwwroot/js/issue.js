let issueForm;
let imagePreview;
let imageFileInput;

document.addEventListener('DOMContentLoaded', function () {
    issueForm = document.getElementById("issueForm");
    imagePreview = document.getElementById("image-preview");
    imageFileInput = document.getElementById("image-file-input");
    imageFileInput.addEventListener("change", (event) => imageUpload(event), false);
});

function templateSelection() {
    issueForm.method = "GET";
    issueForm.submit();
}

async function imageUpload(e) {
    const file = e.currentTarget.files[0];
    const reader = new FileReader();

    if (file.size > 3145728) { 
        alert('이미지 크기가 3MB를 초과할 수 없습니다.');
        e.currentTarget.value = "";
        return;
    }

    reader.onload = (e) => {
        imagePreview.setAttribute("src", e.target.result);
        imagePreview.setAttribute("data-file", file.name);
    }

    reader.readAsDataURL(file);
}

function submitForm() {
    issueForm.enctype = "multipart/form-data";
    issueForm.action = "issue/request";
    issueForm.method = "POST";
}