# WeAutoMarket Frontend

基于 uniapp + Vue3 的自动化包交易平台前端项目

## 📁 项目结构

```
frontend/src/
├── api/                    # API接口管理
│   └── index.js           # 统一的API接口定义
├── components/            # 通用组件
│   ├── TabBar.vue        # 底部导航栏（支持红点徽章）
│   ├── GoodsCard.vue     # 产品卡片组件
│   ├── MessageItem.vue   # 消息项组件
│   ├── EmptyState.vue    # 空状态组件
│   └── LoadingState.vue  # 加载状态组件
├── composables/          # 组合式API
│   ├── useBadge.js      # 红点徽章管理
│   ├── useMessage.js    # 消息管理
│   └── useOrder.js      # 订单管理
├── constants/            # 常量定义
│   └── index.js         # 项目常量（状态、路由等）
├── mock/                 # 模拟数据
│   └── index.js         # 统一的模拟数据管理
├── pages/               # 页面组件
│   ├── index/           # 首页
│   ├── category/        # 分类页
│   ├── message/         # 消息页
│   ├── user/            # 用户页
│   ├── goods-detail/    # 产品详情
│   ├── search/          # 搜索页
│   ├── order-list/      # 订单列表
│   └── chat-detail/     # 聊天详情
├── static/              # 静态资源
├── utils/               # 工具函数
│   ├── router.js        # 路由管理
│   └── orderStatus.js   # 订单状态配置
├── App.vue              # 应用根组件
├── main.js              # 应用入口
├── pages.json           # 页面配置
└── uni.scss             # 全局样式
```

## 🔴 红点徽章系统

### 功能特点

1. **统一管理**: 使用 `useBadge` 组合式API统一管理所有红点状态
2. **响应式更新**: 基于Vue3响应式系统，数据变化自动更新UI
3. **类型安全**: 使用TypeScript类型定义，确保数据类型正确
4. **动画效果**: 红点支持脉冲动画，提升用户体验
5. **智能显示**: 支持数字徽章和小红点两种显示模式

### 使用方式

#### 1. 在组件中使用红点

```javascript
import { useGlobalBadge } from '@/composables/useBadge'

export default {
  setup() {
    const { tabBarBadges, getBadgeCount } = useGlobalBadge()
    
    return {
      tabBarBadges,
      getBadgeCount
    }
  }
}
```

#### 2. 更新红点数量

```javascript
const { updateMessageBadge, updateOrderBadge } = useGlobalBadge()

// 更新消息红点
updateMessageBadge('chat', 5)

// 更新订单红点
updateOrderBadge('pending', 3)
```

#### 3. 清除红点

```javascript
const { clearBadge } = useGlobalBadge()

// 清除所有消息红点
clearBadge('message')

// 清除特定类型的红点
clearBadge('message', 'chat')
```

### 红点类型

#### 消息红点
- `total`: 总未读消息数
- `chat`: 聊天消息未读数
- `system`: 系统消息未读数
- `service`: 客服消息未读数
- `order`: 订单消息未读数

#### 订单红点
- `pending`: 待付款订单数
- `processing`: 待交付订单数
- `shipping`: 待收货订单数
- `refunding`: 退款中订单数

## 🎨 样式规范

### 红点样式

```scss
.tab-badge {
  position: absolute;
  top: -8rpx;
  right: -12rpx;
  background: #ff4d4f;
  color: #fff;
  font-size: 18rpx;
  font-weight: bold;
  border-radius: 20rpx;
  min-width: 32rpx;
  height: 32rpx;
  animation: badge-pulse 2s infinite;
  
  &--dot {
    min-width: 16rpx;
    height: 16rpx;
    border-radius: 50%;
    animation: dot-pulse 2s infinite;
  }
}
```

### 动画效果

```scss
@keyframes badge-pulse {
  0%, 100% { transform: scale(0.9); }
  50% { transform: scale(1); }
}

@keyframes dot-pulse {
  0%, 100% { transform: scale(1); opacity: 1; }
  50% { transform: scale(1.2); opacity: 0.8; }
}
```

## 🔧 开发指南

### 添加新的红点类型

1. 在 `constants/index.js` 中定义新的常量
2. 在 `useBadge.js` 中添加对应的状态管理
3. 在相关组件中使用新的红点类型

### 自定义红点样式

1. 在组件的样式中覆盖默认样式
2. 使用CSS变量进行主题定制
3. 添加自定义动画效果

## 📱 兼容性

- 微信小程序
- H5
- App (iOS/Android)
- 支付宝小程序
- 百度小程序

## 🚀 性能优化

1. **按需加载**: 组合式API支持按需引入
2. **响应式优化**: 使用computed计算属性避免不必要的重新计算
3. **内存管理**: 全局单例模式，避免重复创建实例
4. **动画优化**: 使用CSS3动画，性能更好

## 📝 更新日志

### v2.0.0 (2024-01-15)
- ✨ 重构项目目录结构，符合前端工程规范
- ✨ 新增红点徽章系统，支持动态显示和更新
- ✨ 使用Vue3组合式API，提升代码复用性
- ✨ 优化TabBar组件，支持红点动画效果
- 🐛 修复购物车功能相关问题
- 📚 完善项目文档和开发指南

### v1.0.0 (2024-01-01)
- 🎉 项目初始化
- ✨ 基础页面和组件开发
- ✨ 消息和订单功能实现
