import axios from 'axios';
import type {
  Product,
  ProductViewModel,
  ProductCategory,
  ProductCategoryViewModel,
  ProductSearchModel,
  Cart,
  CartItem,
  Order,
  Account,
  AccountSearchModel,
  AuthUser,
  Slide,
  SlideQuery,
  Article,
  ArticleViewModel,
  ArticleCategory,
  CustomerDiscount,
  Role,
  OperationResult,
  StockStatus,
  CheckStockRequest,
  CommentModel,
} from '../types';

const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5002';

export const mediaUrl = (key: string): string => {
  if (!key || key.startsWith('http://') || key.startsWith('https://')) {
    return key;
  }

  const encodedKey = key.split('/').map(encodeURIComponent).join('/');
  return `${API_BASE_URL}/api/media/${encodedKey}`;
};

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
  withCredentials: true,
});

api.interceptors.request.use(
  (config) => config,
  (error) => Promise.reject(error)
);

// ==========================================
// Auth APIs
// ==========================================

export const authApi = {
  login: (userName: string, password: string): Promise<OperationResult> =>
    api.post('/api/write/Account/login', { userName, password }).then((res) => res.data),

  register: (data: FormData): Promise<OperationResult> =>
    api.post('/api/write/Account/register', data, {
      headers: { 'Content-Type': 'multipart/form-data' },
    }).then((res) => res.data),

  logout: (): Promise<OperationResult> =>
    api.post('/api/write/Account/logout').then((res) => res.data),

  getCurrentUser: (): Promise<AuthUser> =>
    api.get('/api/write/CurrentUser').then((res) => res.data),
};

// ==========================================
// Account APIs (Admin)
// ==========================================

export const accountApi = {
  search: (params: AccountSearchModel): Promise<Account[]> =>
    api.get('/api/write/Account/search', { params }).then((res) => res.data),

  getAll: (): Promise<Account[]> =>
    api.get('/api/write/Account').then((res) => res.data),

  getDetails: (id: number): Promise<any> =>
    api.get(`/api/write/Account/${id}`).then((res) => res.data),

  edit: (data: FormData): Promise<OperationResult> =>
    api.put('/api/write/Account/edit', data, {
      headers: { 'Content-Type': 'multipart/form-data' },
    }).then((res) => res.data),

  changePassword: (data: { id: number; password: string; rePassword: string }): Promise<OperationResult> =>
    api.post('/api/write/Account/change-password', data).then((res) => res.data),
};

// ==========================================
// Role APIs (Admin)
// ==========================================

export const roleApi = {
  getAll: (): Promise<Role[]> =>
    api.get('/api/write/Role').then((res) => res.data),

  getDetails: (id: number): Promise<any> =>
    api.get(`/api/write/Role/${id}`).then((res) => res.data),

  create: (data: { name: string; permissions: number[] }): Promise<OperationResult> =>
    api.post('/api/write/Role', data).then((res) => res.data),

  edit: (data: { id: number; name: string; permissions: number[] }): Promise<OperationResult> =>
    api.put('/api/write/Role', data).then((res) => res.data),
};

// ==========================================
// Product APIs (Admin)
// ==========================================

export const productApi = {
  getLatest: (): Promise<Product[]> =>
    api.get('/api/read/ProductQuery/latest').then((res) => res.data),

  getBySlug: (slug: string): Promise<Product> =>
    api.get(`/api/read/ProductQuery/${slug}`).then((res) => res.data),

  search: (value: string): Promise<Product[]> =>
    api.get(`/api/read/ProductQuery/search/${value}`).then((res) => res.data),

  checkInventory: (data: CheckStockRequest): Promise<StockStatus> =>
    api.post('/api/read/InventoryQuery/checkstock', data).then((res) => res.data),

  // Admin
  getAll: (): Promise<ProductViewModel[]> =>
    api.get('/api/write/Product').then((res) => res.data),

  searchAdmin: (params: ProductSearchModel): Promise<ProductViewModel[]> =>
    api.get('/api/write/Product/search', { params }).then((res) => res.data),

  getDetails: (id: number): Promise<any> =>
    api.get(`/api/write/Product/${id}`).then((res) => res.data),

  create: (data: FormData): Promise<OperationResult> =>
    api.post('/api/write/Product', data, {
      headers: { 'Content-Type': 'multipart/form-data' },
    }).then((res) => res.data),

  edit: (data: FormData): Promise<OperationResult> =>
    api.put('/api/write/Product', data, {
      headers: { 'Content-Type': 'multipart/form-data' },
    }).then((res) => res.data),
};

// ==========================================
// Product Category APIs
// ==========================================

export const categoryApi = {
  getAll: (): Promise<ProductCategory[]> =>
    api.get('/api/read/ProductCategoryQuery').then((res) => res.data),

  getBySlug: (slug: string): Promise<ProductCategory> =>
    api.get(`/api/read/ProductCategoryQuery/${slug}`).then((res) => res.data),

  getWithProducts: (): Promise<ProductCategory[]> =>
    api.get('/api/read/ProductCategoryQuery/with-products').then((res) => res.data),

  // Admin
  getAllAdmin: (): Promise<ProductCategoryViewModel[]> =>
    api.get('/api/write/ProductCategory').then((res) => res.data),

  getDetails: (id: number): Promise<any> =>
    api.get(`/api/write/ProductCategory/${id}`).then((res) => res.data),

  create: (data: FormData): Promise<OperationResult> =>
    api.post('/api/write/ProductCategory', data, {
      headers: { 'Content-Type': 'multipart/form-data' },
    }).then((res) => res.data),

  edit: (data: FormData): Promise<OperationResult> =>
    api.put('/api/write/ProductCategory', data, {
      headers: { 'Content-Type': 'multipart/form-data' },
    }).then((res) => res.data),

  remove: (id: number): Promise<OperationResult> =>
    api.delete(`/api/write/ProductCategory/${id}`).then((res) => res.data),
};

// ==========================================
// Cart APIs
// ==========================================

export const cartApi = {
  compute: (cartItems: CartItem[]): Promise<Cart> =>
    api.post('/api/read/Cart/compute', { cartItems }).then((res) => res.data),
};

// ==========================================
// Order APIs
// ==========================================

export const orderApi = {
  place: (cart: Cart): Promise<{ orderId: number }> =>
    api.post('/api/write/Order', cart).then((res) => res.data),

  getAmount: (id: number): Promise<{ amount: number }> =>
    api.get(`/api/write/Order/${id}/amount`).then((res) => res.data),

  getItems: (id: number): Promise<Order[]> =>
    api.get(`/api/write/Order/${id}/items`).then((res) => res.data),

  getOrdersByAccount: (accountId: number): Promise<Order[]> =>
    api.get(`/api/read/OrderQuery/account/${accountId}`).then((res) => res.data),

  getPaidOrders: (): Promise<Order[]> =>
    api.get('/api/read/OrderQuery/paid').then((res) => res.data),

  search: (params: any): Promise<Order[]> =>
    api.get('/api/write/Order/search', { params }).then((res) => res.data),
};

// ==========================================
// Slide APIs (Admin)
// ==========================================

export const slideApi = {
  getForQuery: (): Promise<SlideQuery[]> =>
    api.get('/api/read/SlideQuery').then((res) => res.data),

  // Admin
  getAll: (): Promise<Slide[]> =>
    api.get('/api/write/Slide').then((res) => res.data),

  getDetails: (id: number): Promise<Slide> =>
    api.get(`/api/write/Slide/${id}`).then((res) => res.data),

  create: (data: FormData): Promise<OperationResult> =>
    api.post('/api/write/Slide', data, {
      headers: { 'Content-Type': 'multipart/form-data' },
    }).then((res) => res.data),

  edit: (data: FormData): Promise<OperationResult> =>
    api.put('/api/write/Slide', data, {
      headers: { 'Content-Type': 'multipart/form-data' },
    }).then((res) => res.data),

  remove: (id: number): Promise<OperationResult> =>
    api.delete(`/api/write/Slide/${id}`).then((res) => res.data),

  restore: (id: number): Promise<OperationResult> =>
    api.post(`/api/write/Slide/${id}/restore`).then((res) => res.data),
};

// ==========================================
// Article APIs
// ==========================================

export const articleApi = {
  getLatest: (): Promise<Article[]> =>
    api.get('/api/read/ArticleQuery/latest').then((res) => res.data),

  getBySlug: (slug: string): Promise<Article> =>
    api.get(`/api/read/ArticleQuery/${slug}`).then((res) => res.data),

  // Admin
  search: (params: any): Promise<ArticleViewModel[]> =>
    api.get('/api/write/Article/search', { params }).then((res) => res.data),

  getDetails: (id: number): Promise<any> =>
    api.get(`/api/write/Article/${id}`).then((res) => res.data),

  create: (data: FormData): Promise<OperationResult> =>
    api.post('/api/write/Article', data, {
      headers: { 'Content-Type': 'multipart/form-data' },
    }).then((res) => res.data),

  edit: (data: FormData): Promise<OperationResult> =>
    api.put('/api/write/Article', data, {
      headers: { 'Content-Type': 'multipart/form-data' },
    }).then((res) => res.data),
};

// ==========================================
// Article Category APIs
// ==========================================

export const articleCategoryApi = {
  getAll: (): Promise<ArticleCategory[]> =>
    api.get('/api/write/ArticleCategory').then((res) => res.data),

  getDetails: (id: number): Promise<any> =>
    api.get(`/api/write/ArticleCategory/${id}`).then((res) => res.data),

  create: (data: FormData): Promise<OperationResult> =>
    api.post('/api/write/ArticleCategory', data, {
      headers: { 'Content-Type': 'multipart/form-data' },
    }).then((res) => res.data),

  edit: (data: FormData): Promise<OperationResult> =>
    api.put('/api/write/ArticleCategory', data, {
      headers: { 'Content-Type': 'multipart/form-data' },
    }).then((res) => res.data),
};

// ==========================================
// Discount APIs (Admin)
// ==========================================

export const discountApi = {
  search: (params: any): Promise<CustomerDiscount[]> =>
    api.get('/api/write/CustomerDiscount/search', { params }).then((res) => res.data),

  getDetails: (id: number): Promise<CustomerDiscount> =>
    api.get(`/api/write/CustomerDiscount/${id}`).then((res) => res.data),

  create: (data: any): Promise<OperationResult> =>
    api.post('/api/write/CustomerDiscount', data).then((res) => res.data),

  edit: (data: any): Promise<OperationResult> =>
    api.put('/api/write/CustomerDiscount', data).then((res) => res.data),
};

// ==========================================
// Comment APIs
// ==========================================

export const commentApi = {
  add: (data: { name: string; email: string; description: string; ownerRecordId: number; productSlug: string }): Promise<OperationResult> =>
    api.post('/api/write/Comment', data).then((res) => res.data),

  getAll: (): Promise<CommentModel[]> =>
    api.get('/api/write/Comment').then((res) => res.data),

  confirm: (id: number): Promise<OperationResult> =>
    api.post(`/api/write/Comment/${id}/confirm`).then((res) => res.data),

  cancel: (id: number): Promise<OperationResult> =>
    api.post(`/api/write/Comment/${id}/cancel`).then((res) => res.data),
};

export default api;
