.PHONY: copy-env
copy-env:
	cp -n .env.example .env | true

.PHONY: run-infrastructure
run-infrastructure: copy-env
	docker-compose -f docker-compose.infrastructure.yml -f docker-compose.tools.yml up

.PHONY: shutdown-infrastructure
shutdown-infrastructure:
	docker-compose -f docker-compose.infrastructure.yml -f docker-compose.tools.yml down