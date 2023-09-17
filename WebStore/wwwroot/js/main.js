var mainDiv = document.getElementsByClassName("mainDiv")[0];

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
function CreateRegistrationForm()
{
    const form = document.createElement("div");
    form.id = "regForm";
    form.className = "formDiv";

    var div = document.createElement("div");
    div.className = "formGroup";
    var label = document.createElement("label");
    label.innerText = "Name"
    var input = document.createElement("input");
    var span = document.createElement("span");
    span.id = "Name";
    div.append(label, input, span);
    form.append(div);

    div = document.createElement("div");
    div.className = "formGroup";
    label = document.createElement("label");
    label.innerText = "Email";
    input = document.createElement("input");
    span = document.createElement("span");
    span.id = "Email";
    div.append(label, input, span);
    form.append(div);

    div = document.createElement("div");
    div.className = "formGroup";
    label = document.createElement("label");
    label.innerText = "Password";
    input = document.createElement("input");
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
    div.append(login, submit);
    form.append(div);

    mainDiv.append(form);
}

CreateRegistrationForm();

//getAllProducts();