# LampShade Frontend

Modern React + TypeScript frontend for the LampShade e-commerce application, built with Ant Design.

## Technology Stack

- **React 18** with TypeScript
- **Vite** for build tooling
- **Ant Design** for UI components
- **React Router** for routing
- **Axios** for API calls

## Project Structure

```
FrontEnd/
├── src/
│   ├── api/           # API services (Axios)
│   ├── assets/        # Static assets
│   ├── components/    # Reusable components
│   │   ├── common/    # Common components (ProductCard)
│   │   └── layout/    # Layout components (Header, Footer, MainLayout)
│   ├── pages/         # Page components
│   │   ├── home/      # Home page
│   │   ├── products/  # Product listing, detail, search, categories
│   │   ├── cart/      # Shopping cart
│   │   ├── checkout/  # Checkout flow
│   │   ├── auth/      # Login, Register
│   │   └── account/   # Profile, Orders
│   ├── types/         # TypeScript interfaces
│   ├── services/      # API service layer
│   ├── hooks/         # Custom React hooks
│   ├── utils/         # Utility functions
│   └── styles/        # Global styles
├── .env               # Environment variables
├── .env.example       # Example environment variables
├── vite.config.ts     # Vite configuration
└── package.json       # Dependencies
```

## Getting Started

### Prerequisites

- Node.js 18+ 
- npm or yarn

### Installation

```bash
cd FrontEnd
npm install
```

### Development

```bash
npm run dev
```

The frontend will start at `http://localhost:3000` with API proxy to `http://localhost:5002`.

### Build

```bash
npm run build
```

Output will be in `dist/` folder.

## Environment Variables

| Variable | Description | Default |
|----------|-------------|---------|
| `VITE_API_URL` | Backend API URL | `http://localhost:5002` |

## Pages

- **Home** (`/`) - Hero carousel, categories, latest products
- **Products** (`/products`) - Product listing with filters and sorting
- **Product Detail** (`/product/:slug`) - Product details with images, reviews
- **Categories** (`/categories`) - All product categories
- **Category** (`/category/:slug`) - Products in a category
- **Search** (`/search?q=query`) - Search results
- **Cart** (`/cart`) - Shopping cart
- **Checkout** (`/checkout`) - Order placement
- **Login** (`/login`) - User login
- **Register** (`/register`) - User registration
- **Profile** (`/account/profile`) - User profile
- **Orders** (`/account/orders`) - Order history

## API Integration

The frontend connects to the LampShade API backend. All API calls are centralized in `src/services/api.ts`.

### API Endpoints Used

| Endpoint | Method | Description |
|----------|--------|-------------|
| `/api/read/ProductQuery/latest` | GET | Get latest products |
| `/api/read/ProductQuery/{slug}` | GET | Get product by slug |
| `/api/read/ProductQuery/search/{value}` | GET | Search products |
| `/api/read/ProductCategoryQuery` | GET | Get all categories |
| `/api/read/ProductCategoryQuery/{slug}` | GET | Get category with products |
| `/api/read/Cart/compute` | POST | Compute cart totals |
| `/api/read/SlideQuery` | GET | Get slides for carousel |
| `/api/read/ArticleQuery/latest` | GET | Get latest articles |
| `/api/write/Account/login` | POST | User login |
| `/api/write/Account/register` | POST | User registration |
| `/api/write/Account/logout` | POST | User logout |
| `/api/write/Order` | POST | Place order |
| `/api/write/Comment` | POST | Add product comment |

## Backend Requirements

The backend API must be running at `http://localhost:5002` (or the URL specified in `.env`).

CORS is already configured in the backend to allow all origins.

## Features

- Responsive design (mobile, tablet, desktop)
- RTL support
- Ant Design components
- Cart management (localStorage)
- Product search and filtering
- Order placement flow
- User authentication
- Product reviews
