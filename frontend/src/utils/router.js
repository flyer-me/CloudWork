/**
 * 路由管理工具类
 * 统一管理页面跳转，提供更好的用户体验
 */

// 页面路径常量
export const ROUTES = {
  // 主要页面
  INDEX: '/pages/index/index',
  CATEGORY: '/pages/category/category',
  MESSAGE: '/pages/message/message',
  USER: '/pages/user/user',
  
  // 商品相关
  GOODS_DETAIL: '/pages/goods-detail/goods-detail',
  SEARCH: '/pages/search/search',
  
  // 订单相关
  ORDER_LIST: '/pages/order-list/order-list',
  ORDER_DETAIL: '/pages/order-detail/order-detail',
  ORDER_CONFIRM: '/pages/order-confirm/order-confirm',
  
  // 用户相关
  PROFILE: '/pages/profile/profile',
  ADDRESS: '/pages/address/address',
  COUPON: '/pages/coupon/coupon',
  POINTS: '/pages/points/points',
  
  // 消息相关
  CHAT_DETAIL: '/pages/chat-detail/chat-detail',

  // 其他
  HELP: '/pages/help/help',
  PAYMENT: '/pages/payment/payment',
  REVIEW: '/pages/review/review'
}

// Tab页面列表
const TAB_PAGES = [
  ROUTES.INDEX,
  ROUTES.CATEGORY,
  ROUTES.MESSAGE,
  ROUTES.USER
]

/**
 * 路由跳转类
 */
class Router {
  /**
   * 普通页面跳转
   * @param {string} url 页面路径
   * @param {object} params 参数对象
   * @param {object} options 跳转选项
   */
  static navigateTo(url, params = {}, options = {}) {
    const fullUrl = this.buildUrl(url, params)
    
    return new Promise((resolve, reject) => {
      uni.navigateTo({
        url: fullUrl,
        animationType: options.animationType || 'slide-in-right',
        animationDuration: options.animationDuration || 300,
        success: resolve,
        fail: reject
      })
    })
  }

  /**
   * 重定向跳转（关闭当前页面）
   * @param {string} url 页面路径
   * @param {object} params 参数对象
   */
  static redirectTo(url, params = {}) {
    const fullUrl = this.buildUrl(url, params)
    
    return new Promise((resolve, reject) => {
      uni.redirectTo({
        url: fullUrl,
        success: resolve,
        fail: reject
      })
    })
  }

  /**
   * 重新加载页面（清空页面栈）
   * @param {string} url 页面路径
   * @param {object} params 参数对象
   */
  static reLaunch(url, params = {}) {
    const fullUrl = this.buildUrl(url, params)
    
    return new Promise((resolve, reject) => {
      uni.reLaunch({
        url: fullUrl,
        success: resolve,
        fail: reject
      })
    })
  }

  /**
   * Tab页面跳转
   * @param {string} url 页面路径
   */
  static switchTab(url) {
    if (!TAB_PAGES.includes(url)) {
      console.warn(`${url} is not a tab page`)
      return Promise.reject(new Error('Not a tab page'))
    }
    
    return new Promise((resolve, reject) => {
      uni.switchTab({
        url,
        success: resolve,
        fail: reject
      })
    })
  }

  /**
   * 返回上一页
   * @param {number} delta 返回层数
   */
  static navigateBack(delta = 1) {
    return new Promise((resolve, reject) => {
      uni.navigateBack({
        delta,
        success: resolve,
        fail: reject
      })
    })
  }

  /**
   * 构建完整URL
   * @param {string} url 基础路径
   * @param {object} params 参数对象
   * @returns {string} 完整URL
   */
  static buildUrl(url, params = {}) {
    if (!params || Object.keys(params).length === 0) {
      return url
    }
    
    const queryString = Object.keys(params)
      .map(key => `${encodeURIComponent(key)}=${encodeURIComponent(params[key])}`)
      .join('&')
    
    return `${url}?${queryString}`
  }

  /**
   * 解析URL参数
   * @param {string} url 完整URL
   * @returns {object} 参数对象
   */
  static parseUrl(url) {
    const [path, queryString] = url.split('?')
    const params = {}
    
    if (queryString) {
      queryString.split('&').forEach(param => {
        const [key, value] = param.split('=')
        params[decodeURIComponent(key)] = decodeURIComponent(value || '')
      })
    }
    
    return { path, params }
  }

  /**
   * 获取当前页面信息
   * @returns {object} 页面信息
   */
  static getCurrentPage() {
    const pages = getCurrentPages()
    const currentPage = pages[pages.length - 1]
    
    return {
      route: currentPage.route,
      options: currentPage.options,
      fullPath: `/${currentPage.route}`,
      params: currentPage.options
    }
  }

  /**
   * 检查是否可以返回
   * @returns {boolean}
   */
  static canGoBack() {
    const pages = getCurrentPages()
    return pages.length > 1
  }

  /**
   * 预加载页面
   * @param {string} url 页面路径
   */
  static preloadPage(url) {
    return new Promise((resolve, reject) => {
      uni.preloadPage({
        url,
        success: resolve,
        fail: reject
      })
    })
  }
}

/**
 * 便捷跳转方法
 */
export const navigate = {
  // 商品详情
  toGoodsDetail: (goodsId, params = {}) => {
    return Router.navigateTo(ROUTES.GOODS_DETAIL, { id: goodsId, ...params })
  },
  
  // 搜索页面
  toSearch: (keyword = '') => {
    return Router.navigateTo(ROUTES.SEARCH, keyword ? { keyword } : {})
  },
  
  // 订单列表
  toOrderList: (status = 'all') => {
    return Router.navigateTo(ROUTES.ORDER_LIST, { status })
  },
  
  // 订单详情
  toOrderDetail: (orderId) => {
    return Router.navigateTo(ROUTES.ORDER_DETAIL, { id: orderId })
  },
  
  // 确认订单
  toOrderConfirm: (goodsId, params = {}) => {
    return Router.navigateTo(ROUTES.ORDER_CONFIRM, { goodsId, ...params })
  },
  
  // 支付页面
  toPayment: (orderId) => {
    return Router.navigateTo(ROUTES.PAYMENT, { orderId })
  },
  
  // 用户资料
  toProfile: () => {
    return Router.navigateTo(ROUTES.PROFILE)
  },
  
  // 收货地址
  toAddress: () => {
    return Router.navigateTo(ROUTES.ADDRESS)
  },
  
  // 优惠券
  toCoupon: () => {
    return Router.navigateTo(ROUTES.COUPON)
  },
  
  // 积分页面
  toPoints: () => {
    return Router.navigateTo(ROUTES.POINTS)
  },
  
  // 帮助中心
  toHelp: () => {
    return Router.navigateTo(ROUTES.HELP)
  },
  
  // 评价页面
  toReview: (orderId) => {
    return Router.navigateTo(ROUTES.REVIEW, { orderId })
  },
  
  // 消息相关
  toMessage: () => Router.switchTab(ROUTES.MESSAGE),
  toChatDetail: (messageId, senderName) => {
    return Router.navigateTo(ROUTES.CHAT_DETAIL, { id: messageId, name: senderName })
  },

  // Tab页面跳转
  toIndex: () => Router.switchTab(ROUTES.INDEX),
  toCategory: () => Router.switchTab(ROUTES.CATEGORY),
  toMessage: () => Router.switchTab(ROUTES.MESSAGE),
  toUser: () => Router.switchTab(ROUTES.USER),
  
  // 返回
  back: (delta = 1) => Router.navigateBack(delta)
}

export default Router
