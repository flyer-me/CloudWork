# WeAutoMarket - 自动化包交易平台

##  项目简介
WeAutoMarket 是一个自动化包交易平台。  
用户可在平台发布、获取自动化任务（如Excel生成、批量操作、网页自动化等），并追踪任务执行状态。  
该项目使用 **uniapp前端 + Spring 后端**构建。

---

##  核心功能
- **用户管理**：微信登录，角色权限（普通用户 / 商家 / 管理员）
- **自动化包市场**：搜索、上架、下架、购买
- **自动化包交付**：
  - 制品上传云存储、云盘，提供下载链接
  - 根据包标签，如Win/VBA，提供预制的保底使用文档
  - 用户反馈
- **CI/CD**：
  - GitHub Actions 自动构建 + 部署
  - Docker 镜像构建
  - 测试 + 代码质量检查
- **后台管理**：
  - 数据统计

---

##  架构设计

### 技术栈
| 模块 | 方案 |
|------|------|
| 前端 | uniapp |
| 后端 | Spring Boot 3 + MyBatis |
| 数据库 | PostgreSQL |
| 缓存 | Redis |
| 队列 | RabbitMQ |
| 容器化 | Docker + Kubernetes |
| CI/CD | GitHub Actions |
| 监控 | Prometheus + Grafana |
| RPA | Selenium + DrissionPage / Python3 / VBA / C# |

---

## API 文档
API 使用 RESTful 设计，并通过 Swagger 3.0 生成文档。  

## 快速启动
```bash
# 1. 克隆仓库
git clone https://github.com/flyer-me/weautomarket.git
cd weautomarket

# 2. 启动后端
cd backend
mvn clean install
docker-compose up -d db redis mq
java -jar target/backend.jar

# 3. 启动小程序
cd frontend
npm install
npm run dev:wechat
```