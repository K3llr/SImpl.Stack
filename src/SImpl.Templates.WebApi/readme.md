# SImpl WebApi Template

**Docker**

Build an image from this project
```shell
docker build --no-cache -f Dockerfile -t simpl.templates.webapi .
```

Start the container
```shell
docker run -d -p 8080:80 simpl.templates.webapi:latest
```

After starting the container point your browser at localhost:8080
