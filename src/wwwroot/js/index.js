if (navigator.userAgent.match(/(iPod|iPhone|iPad|Macintosh)/) && navigator.userAgent.match(/AppleWebKit/)) {
    document.documentElement.className += " ios-safari";
}
else {
    document.getElementById("non-apple-device-warning").style = "";
}

document.addEventListener('DOMContentLoaded', function () {
    const inputs = document.querySelectorAll('input[type="text"]');

    inputs.forEach((input, index) => {
        input.addEventListener('input', (e) => {
            // 입력된 값이 숫자나 대문자가 아니라면 제거
            input.value = input.value.toUpperCase().replace(/[^A-Z0-9]/, '');

            // 다음 input 필드로 포커스 이동
            if (input.value && index < inputs.length - 1) {
                inputs[index + 1].focus();
            }

            // 모든 필드가 채워졌는지 확인
            if (Array.from(inputs).every(i => i.value)) {
                const code = Array.from(inputs).map(i => i.value).join('');
                window.location.href = `auth?code=${code}`;
            }
        });
    });
});