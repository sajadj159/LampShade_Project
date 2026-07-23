// ==========================================
// Account & Auth Types
// ==========================================

export interface AuthUser {
  id: number;
  username: string;
  fullname: string;
  mobile: string;
  address: string;
  postalCode: string;
  roleId: number;
  role: string;
  permissions: number[];
  profilePhoto: string;
}

export interface Account {
  id: number;
  userName: string;
  fullName: string;
  mobile: string;
  address: string;
  postalCode: string;
  role: string;
  roleId: number;
  profilePhoto: string;
  creationDate: string;
}

export interface AccountSearchModel {
  fullName?: string;
  userName?: string;
  mobile?: string;
  roleId?: number;
}

export interface Role {
  id: number;
  name: string;
  creationDate: string;
}

// ==========================================
// Product Types
// ==========================================

export interface ProductPicture {
  id: number;
  pictureUrl: string;
  pictureAlt: string;
  pictureTitle: string;
}

export interface ProductGalleryImage {
  id: number;
  productId: number;
  product: string;
  pictureUrl: string;
  creationDate: string;
  isRemoved: boolean;
}

export interface Comment {
  id: number;
  name: string;
  email: string;
  description: string;
  rating: number;
}

export interface Product {
  id: number;
  pictureUrl: string;
  pictureAlt: string;
  pictureTitle: string;
  name: string;
  doublePrice: number;
  price: string;
  priceWithDiscount: string;
  discountRate: number;
  category: string;
  slug: string;
  inStock: boolean;
  hasDiscount: boolean;
  discountExpireDate: string;
  shortDescription: string;
  categorySlug: string;
  code: string;
  description: string;
  keywords: string;
  metaDescription: string;
  pictures: ProductPicture[];
  comments: Comment[];
}

export interface ProductViewModel {
  id: number;
  name: string;
  pictureUrl: string;
  code: string;
  category: string;
  categoryId: number;
  creationDate: string;
}

export interface ProductSearchModel {
  name?: string;
  code?: string;
  categoryId?: number;
}

export interface ProductCategory {
  id: number;
  name: string;
  slug: string;
  pictureUrl: string;
  picture?: string;
  description: string;
  keywords: string;
  metaDescription: string;
  products: Product[];
}

export interface ProductCategoryViewModel {
  id: number;
  name: string;
  slug: string;
  pictureUrl: string;
  picture?: string;
  description: string;
  keywords: string;
  metaDescription: string;
}

// ==========================================
// Cart Types
// ==========================================

export interface CartItem {
  id: number;
  name: string;
  unitPrice: number;
  pictureUrl: string;
  count: number;
  totalItemPrice: number;
  isInStock: boolean;
  discountRate: number;
  discountAmount: number;
  itemPayAmount: number;
}

export interface Cart {
  totalAmount: number;
  discountAmount: number;
  payAmount: number;
  paymentMethod: number;
  items: CartItem[];
}

// ==========================================
// Order Types
// ==========================================

export interface Order {
  id: number;
  paymentMethodId: number;
  payAmount: number;
  isPaid: boolean;
  isCanceled: boolean;
  accountFullName?: string;
  creationDate?: string;
  accountId: number;
  totalAmount: number;
  discountAmount: number;
  issueTrackingNo: string;
  paymentProofUrl?: string;
  payDate: string;
}

export interface OrderItem {
  id: number;
  orderId: number;
  productId: number;
  productName: string;
  pictureUrl: string;
  unitPrice: number;
  count: number;
}

// ==========================================
// Slide Types
// ==========================================

export interface Slide {
  id: number;
  pictureUrl: string;
  heading: string;
  title: string;
  text: string;
  link: string;
  btnText: string;
  creationDate: string;
  isRemoved: boolean;
}

export interface SlideQuery {
  id: number;
  pictureUrl: string;
  pictureAlt: string;
  pictureTitle: string;
  heading: string;
  title: string;
  text: string;
  link: string;
  btnText: string;
}

// ==========================================
// Article Types
// ==========================================

export interface Article {
  id: number;
  title: string;
  slug: string;
  picture: string;
  shortDescription: string;
  description: string;
  category: string;
  categorySlug: string;
  publishDate: string;
}

export interface ArticleViewModel {
  id: number;
  title: string;
  pictureUrl: string;
  shortDescription: string;
  publishDate: string;
  createdDate: string;
  category: string;
  categoryId: number;
}

export interface ArticleCategory {
  id: number;
  name: string;
  slug: string;
  picture: string;
  description: string;
  keywords: string;
  metaDescription: string;
}

// ==========================================
// Discount Types
// ==========================================

export interface CustomerDiscount {
  id: number;
  productId: number;
  product: string;
  discountRate: number;
  startDate: string;
  startDateGr: string;
  endDate: string;
  endDateGr: string;
  reason: string;
  creationDate: string;
}

// ==========================================
// Comment Types
// ==========================================

export interface CommentModel {
  id: number;
  name: string;
  email: string;
  website: string;
  description: string;
  rating: number;
  commentDate: string;
  ownerName: string;
  ownerRecordId: number;
  type: number;
  isConfirmed: boolean;
  isCanceled: boolean;
}

export interface Inventory {
  id: number;
  productId: number;
  product: string;
  unitPrice: number;
  inStock: boolean;
  currentCount: number;
  creationDate: string;
}

export interface InventoryOperation {
  id: number;
  operation: boolean;
  count: number;
  operator: string;
  operationDate: string;
  currentCount: number;
  description: string;
}

// ==========================================
// API Response Types
// ==========================================

export interface OperationResult {
  isSucceeded: boolean;
  message: string;
}

export interface CheckInventoryStatus {
  productId: number;
  count: number;
}

export interface StockStatus {
  isStock: boolean;
  productName: string;
}

export interface CheckStockRequest {
  productId: number;
  count: number;
}

// ==========================================
// Menu Types
// ==========================================

export interface MenuItem {
  name: string;
  slug: string;
}

// ==========================================
// Payment Types
// ==========================================

export interface PaymentMethod {
  id: number;
  name: string;
  description: string;
}







