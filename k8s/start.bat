minikube start
kubectl apply -f .
minikube service blazorui-service
sleep(10)
pause
kubectl delete -f .
minikube stop