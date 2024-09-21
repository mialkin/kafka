CONFLUENT_PROJECT := src/Kafka.Confluent.Api
FLOW_PROJECT := src/Kafka.Flow.Api

.PHONY: copy-env
copy-env:
	cp -n .env.example .env | true

.PHONY: run-infrastructure
run-infrastructure: copy-env
	docker compose --file compose.infrastructure.yaml --file compose.tools.yaml up

.PHONY: shutdown-infrastructure
shutdown-infrastructure:
	docker compose --file compose.infrastructure.yaml --file compose.tools.yaml down
	
.PHONY: run-confluent
run-confluent:
	dotnet run --project $(CONFLUENT_PROJECT)

.PHONY: watch-confluent
watch-confluent:
	dotnet watch --project $(CONFLUENT_PROJECT) --no-hot-reload
	
.PHONY: run-flow
run-flow:
	dotnet run --project $(FLOW_PROJECT)

.PHONY: watch-flow
watch-flow:
	dotnet watch --project $(FLOW_PROJECT) --no-hot-reload