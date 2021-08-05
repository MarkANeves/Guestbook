kubectl config use-context minikube
kubectl config use-context globoticketcluster

cd .\Guestbook.Api

helm install guestbook .\Charts
helm upgrade guestbook .\Charts

az aks update -n globoticketcluster -g aks-globoticket-rg --attach-acr globoticketclusteracr
