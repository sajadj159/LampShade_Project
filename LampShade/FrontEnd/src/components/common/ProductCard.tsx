import React from 'react';
import { Link } from 'react-router-dom';
import { Card, Tag, Typography } from 'antd';
import { ShoppingCartOutlined } from '@ant-design/icons';
import { mediaUrl } from '../../services/api';
import type { Product } from '../../types';

const { Meta } = Card;
const { Text } = Typography;

interface ProductCardProps {
  product: Product;
}

const ProductCard: React.FC<ProductCardProps> = ({ product }) => {
  return (
    <Card
      hoverable
      cover={
        <Link to={`/product/${product.slug}`}>
          <div style={{ overflow: 'hidden', height: 200 }}>
            <img
              alt={product.pictureAlt}
              src={mediaUrl(product.pictureUrl)}
              style={{
                width: '100%',
                height: 200,
                objectFit: 'cover',
                transition: 'transform 0.3s',
              }}
              onMouseOver={(e) => {
                (e.target as HTMLImageElement).style.transform = 'scale(1.05)';
              }}
              onMouseOut={(e) => {
                (e.target as HTMLImageElement).style.transform = 'scale(1)';
              }}
            />
          </div>
        </Link>
      }
      actions={[
        <ShoppingCartOutlined key="cart" style={{ fontSize: 18 }} />,
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
