// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Add event listener when input file is changed

const input_image = document.querySelector("input[type=file]");
input_image.addEventListener("change", function () {
    const input_image_file = this.files[0]
    const image_preview = document.querySelector("img")
    const reader = new FileReader();
    reader.addEventListener("load", () => {
            // convert image file to base64 string
            image_preview.src = reader.result;
        },
        false
    )

    if (input_image_file) {
        reader.readAsDataURL(input_image_file);
    }
}
)
    
