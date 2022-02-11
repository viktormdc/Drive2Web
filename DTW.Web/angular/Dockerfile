FROM node:14-alpine AS build
WORKDIR /app

COPY . .

WORKDIR "/app/DTW.Web/angular"

RUN npm install

RUN npm run build -prod

FROM nginx:1.17.1-alpine AS prod-stage

COPY  nginx.conf /etc/nginx/conf.d/default.conf

COPY --from=build /app/DTW.Web/angular/dist/angular /usr/share/nginx/html

EXPOSE 80
EXPOSE 443

CMD ["nginx","-g","daemon off;"]

RUN apk update
RUN apk add curl
RUN apk add nano
