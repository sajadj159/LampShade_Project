import React, { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Button, Typography, InputNumber, Empty, message, Popconfirm, Tag } from 'antd';
import { DeleteOutlined, ShoppingCartOutlined, HomeOutlined, ReloadOutlined, SafetyCertificateOutlined } from '@ant-design/icons';
import { cartApi, mediaUrl } from '../../services/api';
import type { CartItem, Cart } from '../../types';

const { Title, Text } = Typography;

const CartPage: React.FC = () => {
  const navigate = useNavigate();
  const [cartItems, setCartItems] = useState<CartItem[]>([]);
  const [cart, setCart] = useState<Cart | null>(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const storedCart = localStorage.getItem('cartItems');
    if (storedCart) setCartItems(JSON.parse(storedCart));
  }, []);

  const updateQuantity = (id: number, count: number) => {
    const updatedItems = cartItems.map((item) => item.id === id ? { ...item, count } : item);
    setCartItems(updatedItems);
    setCart(null);
    localStorage.setItem('cartItems', JSON.stringify(updatedItems));
  };

  const removeItem = (id: number) => {
    const updatedItems = cartItems.filter((item) => item.id !== id);
    setCartItems(updatedItems);
    setCart(null);
    localStorage.setItem('cartItems', JSON.stringify(updatedItems));
    message.success('Item removed from cart');
  };

  const computeCart = async () => {
    if (!cartItems.length) {
      message.warning('Cart is empty');
      return;
    }

    setLoading(true);
    try {
      setCart(await cartApi.compute(cartItems));
    } catch {
      message.error('Failed to update cart totals');
    } finally {
      setLoading(false);
    }
  };

  const handleCheckout = () => {
    if (!cart) {
      message.warning('Update the cart before continuing to checkout');
      return;
    }
    localStorage.setItem('computedCart', JSON.stringify(cart));
    navigate('/checkout');
  };

  const displayedSubtotal = cart?.totalAmount ?? cartItems.reduce((sum, item) => sum + item.unitPrice * item.count, 0);
  const displayedDiscount = cart?.discountAmount ?? 0;
  const displayedPayable = cart?.payAmount ?? displayedSubtotal;

  return (
    <div className="cart-page">
      <div className="cart-page__breadcrumb">
        <Link to="/"><HomeOutlined /> Home</Link>
        <Text type="secondary">/</Text>
        <ShoppingCartOutlined /> Shopping Cart
      </div>

      <div className="cart-page__heading">
        <Title level={2}>Shopping Cart</Title>
        <Text type="secondary">{cartItems.length} {cartItems.length === 1 ? 'item' : 'items'}</Text>
      </div>

      {!cartItems.length ? (
        <Empty description="Your cart is empty" className="cart-page__empty">
          <Link to="/products"><Button type="primary">Continue Shopping</Button></Link>
        </Empty>
      ) : (
        <div className="cart-layout">
          <section className="cart-items-panel">
            {cartItems.map((item) => (
              <article className="cart-product" key={item.id}>
                <img src={mediaUrl(item.pictureUrl)} alt={item.name} className="cart-product__image" />
                <div className="cart-product__details">
                  <Text className="cart-product__name">{item.name}</Text>
                  <Text type="secondary" className="cart-product__unit-price">Unit price: {item.unitPrice.toLocaleString()} Tomans</Text>
                  <Tag color="green" className="cart-product__stock">In stock</Tag>
                  <div className="cart-product__actions">
                    <InputNumber min={1} max={100} value={item.count} onChange={(value) => updateQuantity(item.id, value || 1)} aria-label="Quantity" />
                    <Popconfirm title="Remove this item?" onConfirm={() => removeItem(item.id)}>
                      <Button type="text" danger icon={<DeleteOutlined />} aria-label="Remove item" />
                    </Popconfirm>
                  </div>
                </div>
                <div className="cart-product__total">
                  <span className="cart-money cart-money--total">{(item.unitPrice * item.count).toLocaleString()} <span className="cart-money__unit">Tomans</span></span>
                </div>
              </article>
            ))}
          </section>

          <aside className="cart-summary-panel">
            <Title level={4}>Payment details</Title>
            <div className="cart-summary-panel__line"><Text>Items subtotal</Text><span className="cart-money">{displayedSubtotal.toLocaleString()} <span className="cart-money__unit">Tomans</span></span></div>
            <div className="cart-summary-panel__line"><Text type="success">Discount</Text><span className="cart-money">-{displayedDiscount.toLocaleString()} <span className="cart-money__unit">Tomans</span></span></div>
            <div className="cart-summary-panel__total"><Text strong>Order total</Text><span className="cart-money cart-money--total">{displayedPayable.toLocaleString()} <span className="cart-money__unit">Tomans</span></span></div>
            <Button block icon={<ReloadOutlined />} loading={loading} onClick={computeCart}>Update totals</Button>
            <Button type="primary" size="large" block onClick={handleCheckout} disabled={!cart} className="cart-summary-panel__checkout">Continue to checkout</Button>
            {!cart && <Text type="secondary" className="cart-summary-panel__hint">Update totals to apply active discounts before checkout.</Text>}
            <div className="cart-summary-panel__notice"><SafetyCertificateOutlined /><span>Secure checkout. Your delivery details are collected before payment.</span></div>
          </aside>
        </div>
      )}
    </div>
  );
};

export default CartPage;

