app {
    name = 'moh-prime'
    version = 'snapshot'
        namespaces {
        'build'{
            namespace = 'dqszvc-dev'
            disposable = true
        }
    }

    git {
        workDir = ['git', 'rev-parse', '--show-toplevel'].execute().text.trim()
        uri = ['git', 'config', '--get', 'remote.origin.url'].execute().text.trim()
        commit = ['git', 'rev-parse', 'HEAD'].execute().text.trim()
        changeId = "${opt.'pr'}"
        ref = opt.'branch'?:"refs/pull/${git.changeId}/head"
        github {
            owner = app.git.uri.tokenize('/')[2]
            name = app.git.uri.tokenize('/')[3].tokenize('.git')[0]
        }
    }

    build {
        env {
            name = 'build'
            id = "pr-${app.git.changeId}"
        }
        id = "${app.name}-${app.build.env.name}-${app.build.env.id}"
        name = "${app.name}"
        version = "${app.build.env.name}-${app.build.env.id}"

        suffix = "-${app.git.changeId}"
        namespace = 'dqszvc-dev'
        timeoutInSeconds = 60*40 // 40 minutes
        templates = [
                /*[
                    'file':'openshift/postgresql.bc.json',
                    'params':[
                        'NAME':"prime-postgresql",
                        'SUFFIX': "${app.build.suffix}",
                        'TAG_NAME':"${app.build.version}"
                    ]
                ],*/
                [
                    'file':'openshift/_nginx.bc.json',
                    'params':[
                            'NAME':"prime-nginx",
                            'SUFFIX': "${app.build.suffix}",
                            'VERSION':"${app.build.version}",
                            'SOURCE_CONTEXT_DIR': "nginx",
                            'SOURCE_REPOSITORY_URL': "${app.git.uri}"
                    ]
                ],
        ]
    }
}