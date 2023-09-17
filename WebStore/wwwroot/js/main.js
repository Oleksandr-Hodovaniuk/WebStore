var mainDiv = document.getElementsByClassName("mainDiv")[0];
var user = null;

//Get all products from database.
async function getAllProducts()
{
    const responce = await fetch("/api/Product",
    {
        method: "GET",
        headers: {"Accept": "application/json", "Content-Type": "application/json"}
    });

    if(responce.ok === true)
    {
        const productList = await responce.json();

        productList.forEach(p => mainDiv.append(displayProduct(p)));
    }
}

//Display product.
function displayProduct(data)
{
    const product = document.createElement("div");
    product.className = "product";

    const verticalContainer = document.createElement("div");
    verticalContainer.className = "verticalContainer";

    const image = document.createElement("img");
    image.src = "data:image/png;base64," + data.image;
    image.className = "image";
    verticalContainer.append(image);

    const name = document.createElement("div");
    name.innerText = data.name;
    name.className = "name";
    verticalContainer.append(name);

    const description = document.createElement("div");
    description.innerText = data.description;
    description.className = "description";
    verticalContainer.append(description);

    const horizontalContainer = document.createElement("div");
    horizontalContainer.className = "horizontalContainer";

    const price = document.createElement("div");
    price.innerText = data.price + " ₴";
    price.className = "price";
    horizontalContainer.append(price);

    const addBtn = document.createElement("button");
    addBtn.innerText = "Add to cart";
    addBtn.className = "add";
    horizontalContainer.append(addBtn);
    
    product.append(verticalContainer, horizontalContainer);

    return product;
}

//Create registration form.
async function CreateRegistrationForm()
{
    const form = document.createElement("div");
    form.id = "regForm";
    form.className = "formDiv";

    var div = document.createElement("div");
    div.className = "formGroup";
    var label = document.createElement("label");
    label.innerText = "Name"
    var input = document.createElement("input");
    input.id = "name";
    var span = document.createElement("span");
    span.id = "Name";
    div.append(label, input, span);
    form.append(div);

    div = document.createElement("div");
    div.className = "formGroup";
    label = document.createElement("label");
    label.innerText = "Email";
    input = document.createElement("input");
    input.id = "email";
    span = document.createElement("span");
    span.id = "Email";
    div.append(label, input, span);
    form.append(div);

    div = document.createElement("div");
    div.className = "formGroup";
    label = document.createElement("label");
    label.innerText = "Password";
    input = document.createElement("input");
    input.id = "password";
    input.type = "password";
    span = document.createElement("span");
    span.id = "Password";
    div.append(label, input, span);
    form.append(div);

    div = document.createElement("div");
    div.className = "formGroup";
    label = document.createElement("label");
    label.innerText = "Confirm Password";
    input = document.createElement("input");
    input.id = "confirmPassword";
    input.type = "password";
    span = document.createElement("span");
    span.id = "ConfirmPassword";
    div.append(label, input, span);
    form.append(div);

    div = document.createElement("div");
    const login = document.createElement("button");
    login.innerText = "Log in";
    login.className = "loginBtn"

    const submit = document.createElement("button");
    submit.innerText = "Submit";
    submit.className = "submitBtn"
    submit.addEventListener("click", callRegisterForm);

    div.append(login, submit);
    form.append(div);

    mainDiv.append(form);
}

//Register a new user.
async function RegisterUser(userName, userEmail, userPassword, userConfirmPassword)
{
    const responce = await fetch("/api/Register", 
    {
        method: "POST",
        headers: {"Accept": "application/json", "Content-Type": "application/json"},
        body: JSON.stringify({
            name: userName,
            email: userEmail,
            password: userPassword,
            confirmPassword: userConfirmPassword
        })
    });

    if(responce.ok === true)
    {
        user = await responce.json();

        mainDiv.removeChild(document.getElementById("regForm"));
    }
    else if(responce.status === 400)
    {
        const errors = await responce.json();

        const spanList = document.querySelectorAll("span");
        
        spanList.forEach(s => {
            s.innerText = "";
            s.previousElementSibling.style.borderColor = "black";
        });

        errors.forEach(e => {
            
            var prop = JSON.stringify(e.propertyName);
            prop = prop.replace(/["']/g, '');
            
            var msg = JSON.stringify(e.errorMessage);
            msg = msg.replace(/["']/g, '');

            var span = document.getElementById(prop);
            span.innerText = msg;
            span.style.color = "red";
        });
    }
}

//Get values from registration form and call registerUser method.
async function callRegisterForm()
{
    const name = document.getElementById("name").value;
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;
    const conformPassword = document.getElementById("confirmPassword").value;

    await RegisterUser(name, email, password, conformPassword);
}

CreateRegistrationForm();

//getAllProducts();