import React from 'react';
import { Link } from 'react-router-dom';
import { Card, message, Tag, Typography } from 'antd';
import { ShoppingCartOutlined } from '@ant-design/icons';
import { mediaUrl } from '../../services/api';
import type { Product } from '../../types';
import { addProductToCart } from '../../utils/cart';

const { Meta } = Card;
const { Text } = Typography;

interface ProductCardProps {
  product: Product;
}

const ProductCard: React.FC<ProductCardProps> = ({ product }) => {
  const handleAddToCart = () => {
    if (!addProductToCart(product)) {
      message.warning('This product is out of stock');
      return;
    }

    message.success('Added to cart');
  };

  return (
    <Card
      hoverable
      cover={
        <Link to={`/product/${product.slug}`}>
          <div className="product-card__image-frame">
            <img
              alt={product.pictureAlt}
              src={mediaUrl(product.pictureUrl)} className="product-card__image"
            />
          </div>
        </Link>
      }
      actions={[
        <ShoppingCartOutlined
          key="cart"
          aria-label="Add to cart"
          style={{ fontSize: 18, opacity: product.inStock ? 1 : 0.45, cursor: product.inStock ? 'pointer' : 'not-allowed' }}
          onClick={handleAddToCart}
        />,
      ]}
      style={{ height: '100%' }}
    >
      {product.hasDiscount && (
        <Tag color="red" style={{ position: 'absolute', top: 8, right: 8, zIndex: 1 }}>
          -{product.discountRate}%
        </Tag>
      )}
      <Meta
        title={
          <Link to={`/product/${product.slug}`} style={{ color: '#333' }}>
            {product.name}
          </Link>
        }
        description={
          <div>
            <Text type="secondary" style={{ display: 'block', marginBottom: 8 }}>
              {product.category}
            </Text>
            <div style={{ display: 'flex', alignItems: 'center', gap: 8 }}>
              {product.hasDiscount ? (
                <>
                  <Text strong style={{ color: '#ff4d4f', fontSize: 16 }}>
                    {product.priceWithDiscount}
                  </Text>
                  <Text delete type="secondary">
                    {product.price}
                  </Text>
                </>
              ) : (
                <Text strong style={{ fontSize: 16 }}>
                  {product.price}
                </Text>
              )}
            </div>
            <Tag color={product.inStock ? 'green' : 'red'} style={{ marginTop: 8 }}>
              {product.inStock ? 'In Stock' : 'Out of Stock'}
            </Tag>
          </div>
        }
      />
    </Card>
  );
};

export default ProductCard;


