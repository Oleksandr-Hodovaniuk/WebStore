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

getAllProducts();