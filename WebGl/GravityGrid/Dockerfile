FROM node:alpine
COPY . /app
WORKDIR /app
RUN npm install
EXPOSE 1234
ENTRYPOINT ["npm", "run", "serve"]