const cookieName = "cart-items";
function addToCart(id, name, price, pictureUrl) {
    debugger;
    let products = $.cookie(cookieName);
    if (products===undefined) {
        products = [];
    } else {
        products = JSON.parse(products);
    }
    const count = $("#productCount").val();
    const currentProduct = products.find(x => x.id === id);
    if (currentProduct !==undefined) {
        products.find(x => x.id === id).count = parseInt(currentProduct.count) + parseInt(count);
    } else {
        const product = {
            id,
            name,
            price,
            pictureUrl,
            count
        }
        products.push(product);
    }
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
}