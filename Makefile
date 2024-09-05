install-node-deps:
	npm install
	cd src/CoolShop.Promotion && npm install
.PHONY: run

install-python-deps:
	pip install -r src/CoolShop.Inference/requirements.txt
.PHONY: install-python-deps
