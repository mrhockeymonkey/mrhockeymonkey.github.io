# README

## Local Environment

https://hub.docker.com/r/confluentinc/cp-kafka/

```yaml
# docker compose
version: "3.7"
services:
  zookeeper:
    image: confluentinc/cp-zookeeper:5.3.1
    hostname: zookeeper
    container_name: zookeeper
    ports:
      - "2181:2181"
    environment:
      ZOOKEEPER_CLIENT_PORT: 2181
      ZOOKEEPER_TICK_TIME: 2000
      
  broker:
    image: confluentinc/cp-kafka:5.3.1
    hostname: broker
    container_name: broker
    depends_on:
      - zookeeper
    ports:
      - "9092:9092"
      - "29092:29092"
    logging: # comment this out to see noisey broker logs
      driver: none
    environment:
      KAFKA_BROKER_ID: 1
      KAFKA_ZOOKEEPER_CONNECT: 'zookeeper:2181'
      # See: https://www.confluent.co.uk/blog/kafka-listeners-explained/
      KAFKA_LISTENER_SECURITY_PROTOCOL_MAP: LISTENER_LOCAL:PLAINTEXT,LISTENER_DOCKER:PLAINTEXT
      KAFKA_ADVERTISED_LISTENERS: LISTENER_LOCAL://localhost:9092,LISTENER_DOCKER://broker:29092
      KAFKA_INTER_BROKER_LISTENER_NAME: LISTENER_LOCAL
      KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR: 1
      KAFKA_OFFSETS_TOPIC_NUM_PARTITION: 2
      KAFKA_GROUP_INITIAL_REBALANCE_DELAY_MS: 0
      KAFKA_CONFLUENT_SUPPORT_METRICS_ENABLE: 0
```

## Command Line

```bash
# list topics 
kafka-topics --zookeeper localhost --describe

# update topics
kafka-topics --zookeeper localhost --alter --topic cmdb_export --partitions 2
```
