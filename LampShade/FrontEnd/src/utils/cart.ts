import type { CartItem, Product } from '../types';

const cartStorageKey = 'cartItems';

export const addProductToCart = (product: Product, quantity = 1) => {
  if (!product.inStock) return false;

  const cartItems: CartItem[] = JSON.parse(localStorage.getItem(cartStorageKey) || '[]');
  const existingItem = cartItems.find((item) => item.id === product.id);

  if (existingItem) {
    existingItem.count += quantity;
  } else {
    cartItems.push({
      id: product.id,
      name: product.name,
      unitPrice: product.doublePrice,
      pictureUrl: product.pictureUrl,
      count: quantity,
      totalItemPrice: 0,
      isInStock: true,
      discountRate: 0,
      discountAmount: 0,
      itemPayAmount: 0,
    });
  }

  localStorage.setItem(cartStorageKey, JSON.stringify(cartItems));
  window.dispatchEvent(new Event('cart-updated'));
  return true;
};