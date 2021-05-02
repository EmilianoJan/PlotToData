FROM mono:6

ENTRYPOINT ["/run.sh"]
CMD [":0"]

RUN apt-get update && \
    apt-get install -y monodevelop monodevelop-nunit gsettings-desktop-schemas && \
    rm -rf /var/lib/apt/lists/*
ADD run.sh /run.sh
