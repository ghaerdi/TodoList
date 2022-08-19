FROM node:16
WORKDIR /home/app
COPY . /home/app
RUN npm ci
EXPOSE 3000
CMD ["npm", "start"]
