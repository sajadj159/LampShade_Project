﻿const cookieName = "cart-items";
function addToCart(id, name, price, pictureUrl) {
    debugger;
    let products = $.cookie(cookieName);
    if (products === undefined) {
        products = [];
    } else {
        products = JSON.parse(products);
    }
    const count = $("#productCount").val();
    const currentProduct = products.find(x => x.id === id);
    if (currentProduct !== undefined) {
        products.find(x => x.id === id).count = parseInt(currentProduct.count) + parseInt(count);
    } else {
        const product = {
            id,
            name,
            unitPrice: price,
            pictureUrl,
            count
        }
        products.push(product);
    }
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();
}

function updateCart() {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);

    $("#cart_items_count").text(products.length);
    let cartItemsWrapper = $("#cart_items_wrapper");
    cartItemsWrapper.html('');
    products.forEach(x => {
        const product = `<div class="single-cart-item ps-scroll">
                                 <a href="javascript:vovalue(0)" class="remove-icon" onClick="removeFromCart('${x.id}')">
                                     <i class="ion-android-close"></i>
                                 </a>
                                 <div class="image">
                                      <a href="single-product.html">
                                              <img src="/ProductPictures/${x.pictureUrl}"
                                                    class="img-fluid" alt="">
                                      </a>
                                 </div>
                                 <div class="content">
                                      <p class="product-title">
                                           <a href="single-product.html">محصول: ${x.name}</a>
                                      </p>
                                      <p class="count"><span>تعداد: ${x.count} </span></p>
                                      <p class="count"><span>قمیت واحد: ${x.unitPrice} </span> تومان</p>
                                 </div>
                               </div>`;
        cartItemsWrapper.append(product);
    });
}
function removeFromCart(id) {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    const itemToRemove = products.findIndex(x => x.id === id);
    products.splice(itemToRemove, 1);
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();
}