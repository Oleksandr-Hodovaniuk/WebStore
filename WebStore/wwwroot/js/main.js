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

    const verticalContainer = document.createElement("div");
    verticalContainer.className = "verticalContainer";

    const image = document.createElement("img");
    image.src = data.image;
    verticalContainer.append(image);

    const name = document.createElement("div");
    name.innerText = data.name;
    verticalContainer.append(name);

    const description = document.createElement("div");
    description.innerText = data.description;
    verticalContainer.append(description);

    const horizontalContainer = document.createElement("div");
    const price = document.createElement("div");
    price.innerText = data.price + " ₴";
    horizontalContainer.append(price);

    const addBtn = document.createElement("button");
    addBtn.innerText = "Add to cart";
    horizontalContainer.append(addBtn);
    
    product.append(verticalContainer, horizontalContainer);

    return product;
}

getAllProducts();