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
    }
}

getAllProducts();