var mainDiv = document.getElementsByClassName("mainDiv")[0];
var user = null;

var signUpBtn = document.getElementById("signUpBtn");
signUpBtn.addEventListener("click", CreateRegistrationForm);

var logInBtn = document.getElementById("logInBtn");
logInBtn.addEventListener("click", createLogInForm);

const logo = document.getElementById("logo");
logo.addEventListener("click", getAllProducts);


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
        while (mainDiv.firstChild) 
        {
            mainDiv.removeChild(mainDiv.firstChild);
        }

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
    while (mainDiv.firstChild) 
    {
        mainDiv.removeChild(mainDiv.firstChild);
    }
    
    const form = document.createElement("div");
    form.id = "regForm";
    form.className = "formDiv";

    var div = document.createElement("div");
    div.className = "formGroup";
    var label = document.createElement("label");
    label.innerText = "Name"
    var input = document.createElement("input");
    input.id = "name";
    input.className = "inputReg";
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
    input.className = "inputReg";
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
    input.className = "inputReg";
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
    input.className = "inputReg";
    input.type = "password";
    span = document.createElement("span");
    span.id = "ConfirmPassword";
    div.append(label, input, span);
    form.append(div);

    div = document.createElement("div");
    const login = document.createElement("button");
    login.innerText = "Log in";
    login.className = "loginBtn"
    login.addEventListener("click", createLogInForm);

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

        displayUserData(user);

        createModalWindow("You was successfully registered");
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
            span.className = "regSpan";
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

//Create modal window.
function createModalWindow(text)
{
    const modal = document.createElement("div");
    modal.className = "modal";

    const modalContent = document.createElement("div");
    modalContent.className = "modalContent";

    const span = document.createElement("span");
    span.className = "close";
    span.innerHTML = "&times";

    const p = document.createElement("p");
    p.className = "modalP";
    p.innerText = text;

    const btn = document.createElement("button");
    btn.id = "okButton";
    btn.innerText = "Ok";

    modalContent.append(span, p, btn);
    modal.append(modalContent);
    mainDiv.append(modal);

    modal.style.display = "block";
      
    span.onclick = function() 
    {
        modal.style.display = "none";
    }
      
    btn.onclick = function() 
    {
        modal.style.display = "none";
    }
      
    window.onclick = function(event) 
    {
        if (event.target === modal) 
        {
            modal.style.display = "none";
        }
    }
}

//Dispaly user's data.
function displayUserData(user)
{
    while (mainDiv.firstChild) 
    {
        mainDiv.removeChild(mainDiv.firstChild);
    }

    const userDiv = document.createElement("div");
    userDiv.className = "userDiv";

    var horizontalContainer = document.createElement("div");
    horizontalContainer.className = "userProfileConteiner";

    const userProfile = document.createElement("div");
    userProfile.innerText = "User profile";
    userProfile.className = "userProfile";
    horizontalContainer.append(userProfile);
    userDiv.append(horizontalContainer);

    horizontalContainer = document.createElement("div");
    horizontalContainer.className = "horizontalContainer";
    var span = document.createElement("span");
    span.innerText = "Id: ";
    span.className = "userSpan";
    const userId = document.createElement("div");
    userId.innerText = user.id;
    userId.className = "userFieldDiv";
    horizontalContainer.append(span, userId);
    userDiv.append(horizontalContainer);

    horizontalContainer = document.createElement("div");
    horizontalContainer.className = "horizontalContainer";
    span = document.createElement("span");
    span.innerText = "Name: ";
    span.className = "userSpan";
    const userName = document.createElement("div");
    userName.innerText = user.name;
    userName.className = "userFieldDiv";
    horizontalContainer.append(span, userName);
    userDiv.append(horizontalContainer);

    horizontalContainer = document.createElement("div");
    horizontalContainer.className = "horizontalContainer";
    span = document.createElement("span");
    span.innerText = "Email: ";
    span.className = "userSpan";
    const userEmail = document.createElement("div");
    userEmail.innerText = user.email;
    userEmail.className = "userFieldDiv";
    horizontalContainer.append(span, userEmail);
    userDiv.append(horizontalContainer);

    mainDiv.append(userDiv);
}

//Create log in form.
function createLogInForm()
{
    while (mainDiv.firstChild) 
    {
        mainDiv.removeChild(mainDiv.firstChild);
    }

    const form = document.createElement("div");
    form.id = "logForm";
    form.className = "formDiv";

    var div = document.createElement("div");
    div.className = "formGroup";
    var label = document.createElement("label");
    label.innerText = "Name"
    var input = document.createElement("input");
    input.id = "name";
    input.className = "inputLog"
    div.append(label, input);
    form.append(div);

    div = document.createElement("div");
    div.className = "formGroup";
    label = document.createElement("label");
    label.innerText = "Password"
    input = document.createElement("input");
    input.id = "password";
    input.className = "inputLog"
    input.type = "password";
    div.append(label, input);
    form.append(div);

    div = document.createElement("div");
    const span = document.createElement("span");
    span.className = "logSpan";
    span.id = "error";
    div.append(span);
    form.append(div); 

    div = document.createElement("div");
    const signUp = document.createElement("button");
    signUp.innerText = "Sign up";
    signUp.className = "loginBtn"
    signUp.addEventListener("click", CreateRegistrationForm);

    const submit = document.createElement("button");
    submit.innerText = "Submit";
    submit.className = "submitBtn"
    submit.addEventListener("click", callLogInForm);

    div.append(signUp, submit);
    form.append(div);

    mainDiv.append(form);
}

//Log in fucntion.
async function logInUser(userName, userPassword)
{
    const responce = await fetch("/api/LogIn", 
    {
        method: "POST",
        headers: {"Accept": "application/json", "Content-Type": "application/json"},
        body: JSON.stringify({
            name: userName,
            password: userPassword
        })
    });

    if(responce.ok === true)
    {
        user = await responce.json();

        while (mainDiv.firstChild) 
        {
            mainDiv.removeChild(mainDiv.firstChild);
        }

        displayUserData(user);

        createModalWindow("You was successfully logged in");
    }
    else if(responce.status === 400)
    {
        const text = await responce.json();

        const errorElement = document.getElementById("error");
        
        errorElement.innerText = text;
    }
}

//Get values from log in form and call lgoInUser method.
async function callLogInForm()
{
    const name = document.getElementById("name").value;
    const password = document.getElementById("password").value;

    await logInUser(name, password);
}


//createLogInForm();

//getAllProducts();