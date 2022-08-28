FROM node:16
WORKDIR /home/app
RUN npm install -g pnpm
COPY package.json pnpm-lock.yaml .
RUN pnpm i --frozen-lockfile
COPY . .
RUN pnpm build
RUN pnpm prune --prod
CMD ["pnpm", "serve"]
